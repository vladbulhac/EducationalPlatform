using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    /// <summary>
    /// Defines the data that is returned as the result of any database operation
    /// </summary>
    public record GetEducationalInstitutionQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<EducationalInstitutionBuilding> BuildingsIDs { get; init; }
    }
}