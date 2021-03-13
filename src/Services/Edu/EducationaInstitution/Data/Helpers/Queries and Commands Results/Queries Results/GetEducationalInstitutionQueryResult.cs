using EducationaInstitutionAPI.Utils.Enums;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    public record GetEducationalInstitutionQueryResult
    {
        public Guid EduInstitutionID { get; init; }
        public ICollection<FieldOfStudy> FieldsOfStudy { get; init; }
        public ICollection<Disciplines> Disciplines { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public string BuildingID { get; init; }
    }
}