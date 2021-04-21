using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetAllEducationalInstitutionsByLocationQueryResult
    {
        public ICollection<GetEducationalInstitutionByLocationQueryResult> EducationalInstitutions { get; init; }
    }
}