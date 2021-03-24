using System;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    /// <summary>
    /// Defines the properties that are returned as the result of a Get by LocationID operation
    /// </summary>
    public record GetEducationalInstitutionByLocationQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string BuildingID { get; init; }
    }
}