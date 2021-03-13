using EducationaInstitutionAPI.Utils.Enums;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    public record GetEducationalInstitutionByLocationQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public ICollection<FieldOfStudy> FieldsOfStudy { get; init; }
        public ICollection<Disciplines> Disciplines { get; init; }
        public string Name { get; private set; }
        public string Description { get; init; }
        public string BuildingID { get; init; }
    }
}