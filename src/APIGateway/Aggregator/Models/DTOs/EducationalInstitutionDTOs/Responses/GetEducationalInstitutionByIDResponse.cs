namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses;

public record GetEducationalInstitutionByIDResponse
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string LocationID { get; init; }
    public DateTime JoinDate { get; init; }
    public EducationalInstitutionBaseResponse ParentInstitution { get; init; }
    public ICollection<string> BuildingsIDs { get; init; }
    public ICollection<EducationalInstitutionBaseResponse> ChildInstitutions { get; init; }
}