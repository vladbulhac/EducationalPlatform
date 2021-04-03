using EducationaInstitutionAPI.Data;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
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
        public ICollection<EduInstitutionBuilding> BuildingsIDs { get; init; }
    }
}