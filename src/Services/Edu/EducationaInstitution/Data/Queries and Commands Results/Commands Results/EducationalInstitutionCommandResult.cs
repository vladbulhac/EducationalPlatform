using System;

namespace EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results
{
    /// <summary>
    /// Defines the data that is returned as the result of a Create operation
    /// </summary>
    public record EducationalInstitutionCommandResult
    {
        public Guid EduInstitutionID { get; init; }
    }
}