using System;

namespace RabbitMQEventBus.IntegrationEvents
{
    public record IntegrationEvent
    {
        public string EventId { get; init; }
        public DateTime TimeIssued { get; private set; }
        public EventTrigger TriggeredBy { get; init; }
        public string Uri { get; init; }
        public string Message { get; init; }

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid().ToString();
            TimeIssued = DateTime.UtcNow;
        }
    }

    public record EventTrigger
    {
        public string Action { get; init; }
        public string ServiceName { get; init; }
    }
}