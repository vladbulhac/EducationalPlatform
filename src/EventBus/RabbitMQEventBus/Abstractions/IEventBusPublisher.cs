using RabbitMQEventBus.IntegrationEvents;
using System.Collections.Generic;

namespace RabbitMQEventBus.Abstractions
{
    public interface IEventBusPublisher
    {
        public void Publish(IntegrationEvent @event);

        public void PublishMultiple(IEnumerable<IntegrationEvent> @events);
    }
}