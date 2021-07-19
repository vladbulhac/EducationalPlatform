using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results
{
    public record GetAllEducationalInstitutionsByLocationQueryResult
    {
        public ICollection<GetEducationalInstitutionByLocationQueryResult> EducationalInstitutions { get; init; }
    }
}