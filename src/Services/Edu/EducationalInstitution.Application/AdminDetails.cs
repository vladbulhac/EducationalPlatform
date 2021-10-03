using System.Collections.Generic;

namespace EducationalInstitution.Application
{
    public record AdminDetails
    {
        public string Identity { get; init; }
        public ICollection<string> Permissions { get; init; }
    }
}