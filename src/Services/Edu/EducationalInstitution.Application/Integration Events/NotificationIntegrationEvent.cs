using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace EducationalInstitution.Application.Integration_Events
{
    public record NotificationIntegrationEvent : IntegrationEvent
    {
        public ICollection<string> ToNotify { get; init; }
    }
}