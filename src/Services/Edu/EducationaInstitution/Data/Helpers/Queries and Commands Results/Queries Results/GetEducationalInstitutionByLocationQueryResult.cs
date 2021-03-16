using System;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    public record GetEducationalInstitutionByLocationQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string BuildingID { get; init; }
    }
}