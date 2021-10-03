using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace Identity.API.Application.Integration_Events.Events
{
    public record UpdatedAdminsPermissionsIntegrationEvent : IntegrationEvent
    {
        public Guid EducationalInstitutionId { get; init; }
        public ICollection<AdminDetailsForIntegrationEvent> UpdatedAdmins { get; init; }
    }
}