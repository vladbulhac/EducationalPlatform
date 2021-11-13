namespace EducationalInstitution.Application.Commands.Results;

public record DisableEducationalInstitutionCommandResult
{
    public DateTime DateForPermanentDeletion { get; init; }
}