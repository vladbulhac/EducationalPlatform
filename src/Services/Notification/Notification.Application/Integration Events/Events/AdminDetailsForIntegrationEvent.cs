using System.Collections.Generic;

namespace EducationalInstitution.Application.Integration_Events
{
    public record AdminDetailsForIntegrationEvent : AdminDetails
    {
        public string DetailedMessage { get; init; }
    }
    public record AdminDetails
    {
        public string Identity { get; init; }
        public ICollection<string> Permissions { get; init; }
    }
}