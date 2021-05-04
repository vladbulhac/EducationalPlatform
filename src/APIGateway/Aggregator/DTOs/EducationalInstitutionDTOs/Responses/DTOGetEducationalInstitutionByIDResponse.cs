using System;
using System.Collections.Generic;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record DTOGetEducationalInstitutionByIDResponse
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public DateTime JoinDate { get; init; }
        public DTOEducationalInstitutionBaseResponse ParentInstitution { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }
        public ICollection<DTOEducationalInstitutionBaseResponse> ChildInstitutions { get; init; }
    }
}