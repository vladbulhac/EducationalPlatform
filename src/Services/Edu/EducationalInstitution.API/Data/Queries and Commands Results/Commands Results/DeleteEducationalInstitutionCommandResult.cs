using System;

namespace EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results
{
    public record DeleteEducationalInstitutionCommandResult
    {
        public DateTime DateForPermanentDeletion { get; init; }
    }
}