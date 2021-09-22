using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace Notification.Application.Integration_Events.Events
{
    public record NotificationIntegrationEvent : IntegrationEvent
    {
        public ICollection<string> ToNotify { get; init; }
    }
}