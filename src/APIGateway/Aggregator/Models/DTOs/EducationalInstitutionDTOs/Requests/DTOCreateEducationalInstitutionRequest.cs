namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests;

public record struct DTOCreateEducationalInstitutionRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string LocationID { get; init; }
    public ICollection<string> BuildingsIDs { get; init; }
    public Guid? ParentInstitutionID { get; init; }
}