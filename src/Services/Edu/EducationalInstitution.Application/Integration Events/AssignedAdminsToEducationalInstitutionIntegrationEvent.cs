using RabbitMQEventBus.IntegrationEvents;

namespace EducationalInstitution.Application.Integration_Events;
public record AssignedAdminsToEducationalInstitutionIntegrationEvent : IntegrationEvent
{
    public Guid EducationalInstitutionId { get; init; }
    public ICollection<AdminDetailsForIntegrationEvent> NewAdmins { get; init; }

    public AssignedAdminsToEducationalInstitutionIntegrationEvent() { }
    public AssignedAdminsToEducationalInstitutionIntegrationEvent(ICollection<AdminDetailsForIntegrationEvent> newAdmins, Guid educationalInstitutionId, string uri, string action, string message = "Admin rights granted for an Educational Institution!", string serviceName = default)
    {
        NewAdmins = newAdmins;
        EducationalInstitutionId = educationalInstitutionId;
        Message = message;
        Uri = uri;
        TriggeredBy = new()
        {
            Action = action,
            ServiceName = serviceName ?? this.GetType().Namespace.Split('.')[0]
        };
    }
}