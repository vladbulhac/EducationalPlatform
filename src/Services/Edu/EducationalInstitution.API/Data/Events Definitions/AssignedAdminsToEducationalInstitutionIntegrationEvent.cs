using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Business.IntegrationEvents_Handlers
{
    public record AssignedAdminsToEducationalInstitutionIntegrationEvent : IntegrationEvent
    {
        public ICollection<Guid> ToNotify { get; init; }
    }
}