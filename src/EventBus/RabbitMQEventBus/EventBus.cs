using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using RabbitMQEventBus.IntegrationEvents;
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
        private IModel channelForReceivingEvents;
        private const string exchangeName = "event_bus";
        private readonly IPersistentConnectionHandler connectionHandler;

        public EventBus(string queueName, ILogger<EventBus> logger, IPersistentConnectionHandler connectionHandler, IServiceCollection services)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
            this.connectionHandler = connectionHandler ?? throw new ArgumentNullException(nameof(connectionHandler));

            appServicesProvider = services.BuildServiceProvider();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!connectionHandler.CanEstablishConnection()) return;

            using var channelForPublishingThisEvent = GetPreparedChannelForPublishing(out IBasicProperties properties);

            channelForPublishingThisEvent.BasicPublish(exchange: exchangeName,
                                                       routingKey: @event.GetType().Name,
                                                       mandatory: true,
                                                       basicProperties: properties,
                                                       body: GetEncodedIntegrationEvent(@event));

            logger.LogDebug($"A new {@event.GetType().Name} has been published to RabbitMQ!");
        }

        private IModel GetPreparedChannelForPublishing(out IBasicProperties properties)
        {
            var channel = connectionHandler.CreateChannel();
            channel.ExchangeDeclare(exchange: exchangeName,
                                    type: ExchangeType.Direct);

            properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            logger.LogDebug("A channel was created successfully, continuing with Publishing the event!");
            return channel;
        }

        private static byte[] GetEncodedIntegrationEvent(IntegrationEvent @event)
        {
            var message = JsonConvert.SerializeObject(@event);

            return Encoding.UTF8.GetBytes(message);
        }

        public void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent
                                                  where THandler : class, IIntegrationEventHandler<TEvent>
        {
            if (!connectionHandler.CanEstablishConnection()) return;

            if (channelForReceivingEvents == default || channelForReceivingEvents.IsClosed)
                channelForReceivingEvents = connectionHandler.CreateChannel();

            logger.LogDebug("A channel was created successfully, continuing with Subscribing to event!");

            PrepareChannelForSubscriptionToEvent(eventName: typeof(TEvent).Name);

            var consumer = new AsyncEventingBasicConsumer(channelForReceivingEvents);
            consumer.Received += ConsumerHandleReceivedEvent<TEvent, THandler>;

            channelForReceivingEvents.BasicConsume(queueName,
                                                    autoAck: false,
                                                    consumer);
        }

        private void PrepareChannelForSubscriptionToEvent(string eventName)
        {
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

        private async Task ConsumerHandleReceivedEvent<TEvent, THandler>(object _, BasicDeliverEventArgs eventArgs) where TEvent : IntegrationEvent
                                                                                                                    where THandler : class, IIntegrationEventHandler<TEvent>
        {
            try
            {
                var integrationEvent = ExtractIntegrationEventData<TEvent>(eventArgs.Body);
                var handler = appServicesProvider.GetRequiredService<THandler>();

                await ExecuteHandleMethod(handler, integrationEvent);

                channelForReceivingEvents.BasicAck(deliveryTag: eventArgs.DeliveryTag,
                                                   multiple: false);
                await Task.Yield();
            }
            catch (Exception e)
            {
                logger.LogError("Could not handle the received message, error details=>: {0}", e.Message);
            }
        }

        private static TEvent ExtractIntegrationEventData<TEvent>(ReadOnlyMemory<byte> eventBody) where TEvent : IntegrationEvent
        {
            var bodyCopy = eventBody.ToArray();
            var message = Encoding.UTF8.GetString(bodyCopy);

            return JsonConvert.DeserializeObject<TEvent>(message);
        }

        private static async Task ExecuteHandleMethod<TEvent, THandler>(THandler handler, TEvent integrationEvent) where TEvent : IntegrationEvent
                                                                                                                   where THandler : class, IIntegrationEventHandler<TEvent>
        {
            var method = typeof(THandler).GetMethod("HandleEvent");

            await (Task)method.Invoke(handler, new object[] { integrationEvent });
        }

        public void Dispose()
        {
            if (!disposed)
            {
                connectionHandler.Dispose();
                channelForReceivingEvents.Dispose();
                disposed = true;
            }
        }
    }
}