namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
public record GetEducationalInstitutionByIDQueryResult
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string LocationID { get; init; }
    public DateTime JoinDate { get; init; }
    public EducationalInstitutionBaseQueryResult ParentInstitution { get; init; }
    public ICollection<string> BuildingsIDs { get; init; }
    public ICollection<EducationalInstitutionBaseQueryResult> ChildInstitutions { get; init; }
}