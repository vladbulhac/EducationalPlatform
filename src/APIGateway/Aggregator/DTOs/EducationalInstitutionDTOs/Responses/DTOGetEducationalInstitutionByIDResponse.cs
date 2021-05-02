using System;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record DTOGetEducationalInstitutionByIDResponse
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}