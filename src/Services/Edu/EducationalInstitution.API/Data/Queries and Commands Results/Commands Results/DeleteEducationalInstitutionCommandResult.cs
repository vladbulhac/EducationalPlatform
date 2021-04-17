using System;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results
{
    /// <summary>
    /// Defines the data that is returned as the result of a delete operation
    /// </summary>
    public record DeleteEducationalInstitutionCommandResult
    {
        public DateTime DateForPermanentDeletion { get; init; }
    }
}