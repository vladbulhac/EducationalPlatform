using System;

namespace RabbitMQEventBus.IntegrationEvents
{
    public record IntegrationEvent
    {
        public DateTime TimeIssued { get; private set; }
        public EventTrigger TriggeredBy { get; init; }
        public string Url { get; init; }
        public string Message { get; init; }

        public IntegrationEvent() => TimeIssued = DateTime.UtcNow;
    }

    public record EventTrigger
    {
        public string Action { get; init; }
        public string ServiceName { get; init; }
    }
}