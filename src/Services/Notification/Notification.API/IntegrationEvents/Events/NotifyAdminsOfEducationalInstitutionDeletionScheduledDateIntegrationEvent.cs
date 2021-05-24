using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.API.IntegrationEvents.Events
{
    public record NotifyAdminsOfEducationalInstitutionDeletionScheduledDateIntegrationEvent : IntegrationEvent
    {
        public ICollection<Guid> ToNotify { get; init; }
    }
}