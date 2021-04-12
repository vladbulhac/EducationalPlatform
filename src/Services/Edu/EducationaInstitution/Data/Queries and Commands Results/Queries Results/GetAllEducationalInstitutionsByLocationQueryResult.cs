using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.Out
{
    /// <summary>
    /// Defines the data that is returned as the result of a Get by LocationID operation
    /// </summary>
    public record GetAllEducationalInstitutionsByLocationQueryResult
    {
        public ICollection<GetEducationalInstitutionByLocationQueryResult> EducationalInstitutions { get; init; }
    }
}