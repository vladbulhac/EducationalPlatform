using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results
{
    public record GetEducationalInstitutionByLocationQueryResult : EducationalInstitutionBaseQueryResult
    {
        public ICollection<string> BuildingsIDs { get; init; }
    }
}