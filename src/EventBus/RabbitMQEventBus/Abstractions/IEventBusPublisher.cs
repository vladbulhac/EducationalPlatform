using RabbitMQEventBus.IntegrationEvents;

namespace RabbitMQEventBus.Abstractions
{
    public interface IEventBusPublisher
    {
        public void Publish(IntegrationEvent @event);
    }
}