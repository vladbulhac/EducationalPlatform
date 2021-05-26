using System;
using System.Collections.Generic;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record GetAllEducationalInstitutionsByNameResponse
    {
        public ICollection<EducationalInstitutionByNameResponse> EducationalInstitutions { get; init; }
    }

    public record EducationalInstitutionByNameResponse
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
    }
}