using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetAllEducationalInstitutionsByNameQueryResult
    {
        public ICollection<GetEducationalInstitutionQueryResult> EducationalInstitutions { get; init; }
    }
}