using System;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetEducationalInstitutionQueryResult
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
    }
}