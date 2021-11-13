namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;

public record GetAllEducationalInstitutionsByNameQueryResult
{
    public ICollection<GetEducationalInstitutionQueryResult> EducationalInstitutions { get; init; }
}