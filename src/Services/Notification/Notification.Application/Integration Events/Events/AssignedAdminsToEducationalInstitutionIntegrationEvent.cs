using RabbitMQEventBus.IntegrationEvents;

namespace Notification.Application.Integration_Events.Events;

public record AssignedAdminsToEducationalInstitutionIntegrationEvent : IntegrationEvent
{
    public Guid EducationalInstitutionId { get; init; }
    public ICollection<AdminDetailsForIntegrationEvent> NewAdmins { get; init; }
}