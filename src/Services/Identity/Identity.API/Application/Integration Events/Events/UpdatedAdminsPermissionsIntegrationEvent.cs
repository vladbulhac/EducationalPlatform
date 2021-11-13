using RabbitMQEventBus.IntegrationEvents;

namespace Identity.API.Application.Integration_Events.Events;

public record UpdatedAdminsPermissionsIntegrationEvent : IntegrationEvent
{
    public Guid EducationalInstitutionId { get; init; }
    public ICollection<AdminDetailsForIntegrationEvent> UpdatedAdmins { get; init; }
}