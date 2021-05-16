using System;

namespace RabbitMQEventBus.IntegrationEvents
{
    public record IntegrationEvent
    {
        public DateTime TimeIssued { get; private set; }
        public string TriggeredByAction { get; init; }
        public string TriggeredByService_Name { get; init; }
        public string Url { get; init; }
        public string Message { get; init; }

        public IntegrationEvent()
        {
            TimeIssued = DateTime.UtcNow;
        }
    }
}