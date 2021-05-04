using System;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record DTOCreateEducationalInstitutionResponse
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}