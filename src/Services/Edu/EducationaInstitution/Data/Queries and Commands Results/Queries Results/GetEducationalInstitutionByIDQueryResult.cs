using EducationaInstitutionAPI.Data;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    /// <summary>
    /// Defines the properties that are returned as the result of a Get by ID operation
    /// </summary>
    public record GetEducationalInstitutionByIDQueryResult
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public string BuildingID { get; init; }
        public ICollection<Student> Students { get; init; }
        public ICollection<Professor> Professors { get; init; }
        public ICollection<Staff> Personnel { get; init; }
    }
}