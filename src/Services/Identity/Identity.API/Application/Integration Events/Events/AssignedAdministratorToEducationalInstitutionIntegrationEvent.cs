using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace Identity.API.Application.Integration_Events.Events
{
    public record AssignedAdministratorToEducationalInstitutionIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public Guid EducationalInstitutionId { get; init; }
        public ICollection<string> Permissions { get; init; }
    }
}