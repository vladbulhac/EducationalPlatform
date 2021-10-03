using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Integration_Events
{
    public record UpdatedAdminsPermissionsIntegrationEvent : IntegrationEvent
    {
        public Guid EducationalInstitutionId { get; init; }
        public ICollection<AdminDetailsForIntegrationEvent> UpdatedAdmins { get; init; }
    }
}