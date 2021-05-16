using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace Notification.API.IntegrationEvents.Events
{
    public record AssignedAdminsToEducationalInstitutionIntegrationEvent : IntegrationEvent
    {
        public ICollection<Guid> ToNotify { get; init; }
    }
}