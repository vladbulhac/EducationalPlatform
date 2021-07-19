using System;

namespace EducationalInstitution.Application.Commands.Results
{
    public record CreateEducationalInstitutionCommandResult
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}