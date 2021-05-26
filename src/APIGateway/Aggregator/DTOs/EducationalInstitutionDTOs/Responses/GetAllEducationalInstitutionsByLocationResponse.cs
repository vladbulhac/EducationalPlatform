using System;
using System.Collections.Generic;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public record GetAllEducationalInstitutionsByLocationResponse
    {
        public ICollection<EducationalInstitutionByLocationResponse> EducationalInstitutions { get; init; }
    }

    public record EducationalInstitutionByLocationResponse
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<string> Buildings { get; init; }
    }
}