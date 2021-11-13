namespace EducationalInstitution.Application.Integration_Events;

public record AdminDetailsForIntegrationEvent : AdminDetails
{
    public string DetailedMessage { get; init; }
}