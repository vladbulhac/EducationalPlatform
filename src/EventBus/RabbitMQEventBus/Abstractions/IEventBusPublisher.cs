using RabbitMQEventBus.IntegrationEvents;

namespace RabbitMQEventBus.Abstractions;

public interface IEventBusPublisher
{
    public void Publish(IntegrationEvent @event, bool publisherConfirms);

    public void PublishMultiple(IEnumerable<IntegrationEvent> @events, bool publisherConfirms);
}