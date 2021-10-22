using RabbitMQEventBus.IntegrationEvents;
using System.Collections.Generic;

namespace RabbitMQEventBus.Abstractions
{
    public interface IEventBusPublisher
    {
        public void Publish(IntegrationEvent @event, bool publisherConfirms);

        public void PublishMultiple(IEnumerable<IntegrationEvent> @events, bool publisherConfirms);
    }
}