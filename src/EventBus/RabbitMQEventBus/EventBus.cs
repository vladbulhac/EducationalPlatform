using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Subscription;
using RabbitMQEventBus.Transactional_Outbox.Services.Outbox_Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQEventBus
{
    public class EventBus : IEventBus
    {
        private bool disposed;

        private readonly ILogger<EventBus> logger;
        private readonly IServiceProvider appServicesProvider;

        private readonly string queueName;
        private const string exchangeName = "event_bus";

        private readonly ISubscriptionManager subscriptionManager;
        private readonly IPersistentConnectionHandler connectionHandler;

        private readonly IIntegrationEventOutboxService outboxService;
        private readonly ConcurrentDictionary<ulong, IntegrationEvent> deliveryTagToEventMap;

        public EventBus(string queueName, ILogger<EventBus> logger, IPersistentConnectionHandler connectionHandler, IServiceCollection services)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
            this.connectionHandler = connectionHandler ?? throw new ArgumentNullException(nameof(connectionHandler));

            appServicesProvider = services.BuildServiceProvider();
            subscriptionManager = new SubscriptionManager();

            connectionHandler.CanEstablishConnection();

            deliveryTagToEventMap = new();
            outboxService = null;
        }

        public EventBus(string queueName, ILogger<EventBus> logger, IPersistentConnectionHandler connectionHandler, IServiceCollection services, IIntegrationEventOutboxService outboxService) : this(queueName, logger, connectionHandler, services)
        {
            this.outboxService = outboxService ?? throw new ArgumentNullException(nameof(outboxService));
        }

        public void Publish(IntegrationEvent @event, bool publisherConfirms)
        {
            if (@event is null) throw new ArgumentNullException(nameof(@event));
            if (!connectionHandler.CanEstablishConnection()) return;

            using var channelForPublishingThisEvent = connectionHandler.GetTransientChannel();
            logger.LogDebug("[EventBus]: A channel was created successfully, continuing with Publishing the event!");

            if (publisherConfirms) SetupPublisherConfirms(channelForPublishingThisEvent);

            Publish(@event, channelForPublishingThisEvent, publisherConfirms);
        }

        public void PublishMultiple(IEnumerable<IntegrationEvent> @events, bool publisherConfirms)
        {
            if (@events is null) throw new ArgumentException(nameof(@events));
            if (!connectionHandler.CanEstablishConnection()) return;

            using var channelForPublishingThisEvents = connectionHandler.GetTransientChannel();
            logger.LogDebug("[EventBus]: A channel was created successfully, continuing with Publishing the events!");

            if (publisherConfirms) SetupPublisherConfirms(channelForPublishingThisEvents);

            foreach (var @event in @events)
                Publish(@event, channelForPublishingThisEvents, publisherConfirms);
        }

        #region Publish methods

        private void Publish(IntegrationEvent @event, IModel channelForPublishingThisEvent, bool publisherConfirms)
        {
            channelForPublishingThisEvent.ExchangeDeclare(exchange: exchangeName,
                                                          type: ExchangeType.Direct);

            if (publisherConfirms)
                deliveryTagToEventMap.TryAdd(channelForPublishingThisEvent.NextPublishSeqNo, @event);

            channelForPublishingThisEvent.BasicPublish(exchange: exchangeName,
                                                       routingKey: @event.GetType().Name,
                                                       mandatory: true,
                                                       basicProperties: ConfigureMessageProperties(channelForPublishingThisEvent),
                                                       body: GetEncodedIntegrationEvent(@event));

            logger.LogDebug($"[EventBus]: Trying to publish {@event.GetType().Name} to RabbitMQ, awaiting for acknowledgement: {publisherConfirms}!");
        }

        #region Event publish acknowledgement

        private void SetupPublisherConfirms(IModel channel)
        {
            if (outboxService is null) throw new InvalidOperationException($"An instance of {nameof(outboxService)} has not been provided!");

            channel.ConfirmSelect();

            channel.BasicAcks += AcknowledgedEventWasPublished;
            channel.BasicNacks += NotAcknowledgedEventWasPublished;
        }

        private async void AcknowledgedEventWasPublished(object _, BasicAckEventArgs eventArgs) => await SetPublishStatusBasedOnAcknowledgement(eventArgs.DeliveryTag, eventArgs.Multiple, MarkEventAsPublished);

        private async void NotAcknowledgedEventWasPublished(object _, BasicNackEventArgs eventArgs) => await SetPublishStatusBasedOnAcknowledgement(eventArgs.DeliveryTag, eventArgs.Multiple, MarkEventAsNotPublished);

        private async Task SetPublishStatusBasedOnAcknowledgement(ulong deliveryTag, bool multiple, SetPublishStatus SetPublishStatusDelegate)
        {
            if (multiple)
            {
                foreach (var publishedEvent in deliveryTagToEventMap.Where(k => k.Key <= deliveryTag))
                    await SetPublishStatusDelegate(publishedEvent.Key, publishedEvent.Value);
            }
            else
            {
                if (deliveryTagToEventMap.TryGetValue(deliveryTag, out IntegrationEvent @event))
                    await SetPublishStatusDelegate(deliveryTag, @event);
            }
        }

        private delegate Task SetPublishStatus(ulong deliveryTag, IntegrationEvent @event);

        private async Task MarkEventAsPublished(ulong deliveryTag, IntegrationEvent @event)
        {
            await outboxService.EventHasBeenPublished(@event.EventId);
            deliveryTagToEventMap.TryRemove(deliveryTag, out IntegrationEvent _);

            logger.LogInformation($"[EventBus][Success]: Event {@event} with Delivery Tag (Sequence Number): {deliveryTag} has been published to the event bus!");
        }

        private async Task MarkEventAsNotPublished(ulong deliveryTag, IntegrationEvent @event)
        {
            await outboxService.PublishingFailed(@event.EventId);
            deliveryTagToEventMap.TryRemove(deliveryTag, out IntegrationEvent _);

            logger.LogInformation($"[EventBus][Error]: Event {@event.EventId} with Delivery Tag (Sequence Number): {deliveryTag} has NOT been published to the event bus!");
        }

        #endregion Event publish acknowledgement

        private static IBasicProperties ConfigureMessageProperties(IModel channel)
        {
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            return properties;
        }

        private static byte[] GetEncodedIntegrationEvent(IntegrationEvent @event)
        {
            var message = JsonConvert.SerializeObject(@event);

            return Encoding.UTF8.GetBytes(message);
        }

        #endregion Publish methods

        public void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent
                                                  where THandler : IIntegrationEventHandler<TEvent>
        {
            if (!connectionHandler.CanEstablishConnection()) return;

            var eventType = typeof(TEvent);
            ConfigureExchangeAndQueue(eventName: eventType.Name);

            subscriptionManager.SaveSubscription(eventType, typeof(THandler));

            RegisterConsumer();
        }

        #region Subscribe methods

        private void ConfigureExchangeAndQueue(string eventName)
        {
            var channelForReceivingEvents = connectionHandler.GetPersistentChannel();

            channelForReceivingEvents.ExchangeDeclare(exchange: exchangeName,
                                                      type: ExchangeType.Direct);

            channelForReceivingEvents.QueueDeclare(queue: queueName,
                                                   durable: true,
                                                   exclusive: false,
                                                   autoDelete: false,
                                                   arguments: null);

            channelForReceivingEvents.QueueBind(queue: queueName,
                                                exchange: exchangeName,
                                                routingKey: eventName);
        }

        private void RegisterConsumer()
        {
            var channelForReceivingEvents = connectionHandler.GetPersistentChannel();

            var consumer = new AsyncEventingBasicConsumer(channelForReceivingEvents);
            consumer.Received += ConsumerHandleReceivedEvent;

            channelForReceivingEvents.BasicConsume(queueName,
                                                   autoAck: false,
                                                   consumer);
        }

        private async Task ConsumerHandleReceivedEvent(object _, BasicDeliverEventArgs eventArgs)
        {
            try
            {
                var subscription = subscriptionManager.GetSubscriptionDetailsOfEvent(eventName: eventArgs.RoutingKey);

                if (subscription is not null && subscription.Handlers.Count > 0)
                {
                    var integrationEvent = ExtractIntegrationEventData(subscription.EventType, eventArgs.Body);

                    foreach (var handlerType in subscription.Handlers)
                        await ExecuteHandleMethod(handlerType, integrationEvent);

                    connectionHandler.GetPersistentChannel()
                                     .BasicAck(deliveryTag: eventArgs.DeliveryTag,
                                               multiple: false);
                }

                await Task.Yield();
            }
            catch (Exception e)
            {
                logger.LogError("[EventBus][Error]: An error occurred while handling the received event from RabbitMQ event bus, error details => {0}", e.Message);
            }
        }

        private static object ExtractIntegrationEventData(Type eventType, ReadOnlyMemory<byte> eventBody)
        {
            var bodyCopy = eventBody.ToArray();
            var message = Encoding.UTF8.GetString(bodyCopy);

            return JsonConvert.DeserializeObject(message, eventType);
        }

        private async Task ExecuteHandleMethod(Type handlerType, object integrationEvent)
        {
            var method = handlerType.GetMethod("HandleEvent");
            var handler = appServicesProvider.GetRequiredService(handlerType);

            await (Task)method.Invoke(handler, new object[] { integrationEvent });
        }

        #endregion Subscribe methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    subscriptionManager.ClearResources();
                    deliveryTagToEventMap.Clear();

                    outboxService.Dispose();
                    connectionHandler.Dispose();
                }
            }

            disposed = true;
        }
    }
}