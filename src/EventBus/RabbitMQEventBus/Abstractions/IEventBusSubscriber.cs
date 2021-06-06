using RabbitMQEventBus.IntegrationEvents;

namespace RabbitMQEventBus.Abstractions
{
    public interface IEventBusSubscriber
    {
        public void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent
                                                  where THandler : IIntegrationEventHandler<TEvent>;
    }
}