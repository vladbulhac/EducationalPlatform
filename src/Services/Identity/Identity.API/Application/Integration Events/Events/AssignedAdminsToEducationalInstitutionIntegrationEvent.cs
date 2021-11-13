using RabbitMQEventBus.IntegrationEvents;

namespace Identity.API.Application.Integration_Events.Events;

public record AssignedAdminsToEducationalInstitutionIntegrationEvent : IntegrationEvent
{
    public Guid EducationalInstitutionId { get; init; }
    public ICollection<AdminDetailsForIntegrationEvent> NewAdmins { get; init; }
}