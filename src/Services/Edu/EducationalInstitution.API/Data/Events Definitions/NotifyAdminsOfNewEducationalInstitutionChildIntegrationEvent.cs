using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Data.Events_Definitions
{
    public record NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent : IntegrationEvent
    {
        public ICollection<Guid> ToNotify { get; init; }
    }
}