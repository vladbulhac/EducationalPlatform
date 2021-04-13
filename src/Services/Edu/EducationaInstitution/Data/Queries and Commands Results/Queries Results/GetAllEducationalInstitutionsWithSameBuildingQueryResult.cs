using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results
{
    public record GetAllEducationalInstitutionsWithSameBuildingQueryResult
    {
        public ICollection<EducationalInstitutionEssentialData> EducationalInstitutions { get; init; }
    }

    public record EducationalInstitutionEssentialData
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}