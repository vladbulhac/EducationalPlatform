using System;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results
{
    /// <summary>
    /// Defines the data that is returned as the result of any operation
    /// </summary>
    public record EducationalInstitutionCommandResult
    {
        public Guid EduInstitutionID { get; init; }
    }
}