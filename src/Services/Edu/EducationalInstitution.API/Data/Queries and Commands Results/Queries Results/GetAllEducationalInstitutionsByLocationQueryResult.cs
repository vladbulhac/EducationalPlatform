using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    /// <summary>
    /// Defines the data that is returned as the result of a Get by LocationID operation
    /// </summary>
    public record GetAllEducationalInstitutionsByLocationQueryResult
    {
        public ICollection<GetEducationalInstitutionByLocationQueryResult> EducationalInstitutions { get; init; }
    }
}