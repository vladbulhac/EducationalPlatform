using System;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Requests
{
    public record DTOUpdateEducationalInstitutionParentRequest
    {
        public Guid ParentInstitutionID { get; init; }
    }
}