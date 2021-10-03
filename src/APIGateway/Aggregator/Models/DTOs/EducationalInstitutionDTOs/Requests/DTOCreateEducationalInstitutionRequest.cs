using System;
using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests
{
    public record DTOCreateEducationalInstitutionRequest
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }
        public Guid? ParentInstitutionID { get; init; }
    }
}