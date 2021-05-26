using System;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record CreateEducationalInstitutionResponse
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}