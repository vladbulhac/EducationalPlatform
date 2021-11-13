namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
public record GetEducationalInstitutionQueryResult
{
    public Guid EducationalInstitutionID { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string LocationID { get; init; }
}