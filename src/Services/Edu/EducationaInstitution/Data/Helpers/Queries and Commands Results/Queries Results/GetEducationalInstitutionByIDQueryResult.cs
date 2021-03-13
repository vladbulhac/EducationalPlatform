using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Utils.Enums;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    public record GetEducationalInstitutionByIDQueryResult
    {
        public ICollection<FieldOfStudy> FieldsOfStudy { get; init; }
        public ICollection<Disciplines> Disciplines { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public string BuildingID { get; init; }
        public ICollection<Student> Students { get; init; }
        public ICollection<Professor> Professors { get; init; }
        public ICollection<Staff> Personnel { get; init; }
    }
}