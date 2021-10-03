using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace EducationalInstitution.Application.Integration_Events
{
    public record UpdatedAdminsPermissionsIntegrationEvent : IntegrationEvent
    {
        public Guid EducationalInstitutionId { get; init; }
        public ICollection<AdminDetailsForIntegrationEvent> UpdatedAdmins { get; init; }
    }
}