using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    /// <summary>
    /// Defines the data data that is encapsulated in each <see cref="GetAllEducationalInstitutionsByLocationQueryResult"/>'s collection's entry
    /// </summary>
    public record GetEducationalInstitutionByLocationQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<EduInstitutionBuilding> BuildingsIDs { get; init; }
    }
}