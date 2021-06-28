using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using RabbitMQEventBus.IntegrationEvents;
using RabbitMQEventBus.Subscription;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQEventBus
{
    public class EventBus : IEventBus, IDisposable
    {
        private bool disposed;

        private readonly ILogger<EventBus> logger;
        private readonly IServiceProvider appServicesProvider;

        private readonly string queueName;
        private const string exchangeName = "event_bus";

        private readonly ISubscriptionManager subscriptionManager;
        private readonly IPersistentConnectionHandler connectionHandler;

        public EventBus(string queueName, ILogger<EventBus> logger, IPersistentConnectionHandler connectionHandler, IServiceCollection services)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
            this.connectionHandler = connectionHandler ?? throw new ArgumentNullException(nameof(connectionHandler));

            appServicesProvider = services.BuildServiceProvider();
            subscriptionManager = new SubscriptionManager();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!connectionHandler.CanEstablishConnection()) return;

            using var channelForPublishingThisEvent = connectionHandler.GetTransientChannel();
            logger.LogDebug("A channel was created successfully, continuing with Publishing the event!");

            channelForPublishingThisEvent.ExchangeDeclare(exchange: exchangeName,
                                                          type: ExchangeType.Direct);

            channelForPublishingThisEvent.BasicPublish(exchange: exchangeName,
                                                       routingKey: @event.GetType().Name,
                                                       mandatory: true,
                                                       basicProperties: ConfigureMessageProperties(channelForPublishingThisEvent),
                                                       body: GetEncodedIntegrationEvent(@event));

            logger.LogDebug($"A new {@event.GetType().Name} has been published to RabbitMQ!");
        }

        #region Publish methods

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
                logger.LogError("An error occurred while handling the received event from RabbitMQ event bus, error details => {0}", e.Message);
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
            if (!disposed)
            {
                connectionHandler.Dispose();
                disposed = true;
            }
        }
    }
}