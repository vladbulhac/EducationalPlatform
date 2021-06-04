using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Events_Definitions
{
    public record NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent : IntegrationEvent
    {
        public ICollection<Guid> ToNotify { get; init; }
    }
}