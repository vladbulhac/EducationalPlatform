namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests;

public record struct DTOUpdateEducationalInstitutionParentRequest
{
    public Guid ParentInstitutionID { get; init; }
}