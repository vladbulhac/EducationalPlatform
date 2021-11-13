using RabbitMQEventBus.IntegrationEvents;

namespace EducationalInstitution.Application.Integration_Events;

public record UpdatedAdminsPermissionsIntegrationEvent : IntegrationEvent
{
    public Guid EducationalInstitutionId { get; init; }
    public ICollection<AdminDetailsForIntegrationEvent> UpdatedAdmins { get; init; }
}