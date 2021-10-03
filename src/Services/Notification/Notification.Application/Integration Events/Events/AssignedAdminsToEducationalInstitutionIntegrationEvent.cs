using EducationalInstitution.Application.Integration_Events;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace Notification.Application.Integration_Events.Events
{
    public record AssignedAdminsToEducationalInstitutionIntegrationEvent : IntegrationEvent
    {
        public Guid EducationalInstitutionId { get; init; }
        public ICollection<AdminDetailsForIntegrationEvent> NewAdmins { get; init; }
    }
}