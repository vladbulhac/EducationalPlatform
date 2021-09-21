using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses
{
    public record GetAllEducationalInstitutionsByBuildingResponse
    {
        public ICollection<EducationalInstitutionBaseResponse> EducationalInstitutions { get; init; }
    }
}