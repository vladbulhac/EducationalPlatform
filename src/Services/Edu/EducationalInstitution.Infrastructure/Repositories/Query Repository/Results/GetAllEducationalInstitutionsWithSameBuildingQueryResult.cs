using System.Collections.Generic;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results
{
    public record GetAllEducationalInstitutionsWithSameBuildingQueryResult
    {
        public ICollection<EducationalInstitutionBaseQueryResult> EducationalInstitutions { get; init; }
    }
}