using EducationaInstitutionAPI.Data;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    /// <summary>
    /// Defines the data that is returned as the result of a Get by LocationID operation
    /// </summary>
    public record GetEducationalInstitutionByLocationQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<EduInstitutionBuilding> BuildingsIDs { get; init; }
    }
}