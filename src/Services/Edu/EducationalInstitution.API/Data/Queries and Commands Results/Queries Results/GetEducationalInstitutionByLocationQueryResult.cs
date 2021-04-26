using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetEducationalInstitutionByLocationQueryResult : EducationalInstitutionBaseQueryResult
    {
        public ICollection<string> BuildingsIDs { get; init; }
    }
}