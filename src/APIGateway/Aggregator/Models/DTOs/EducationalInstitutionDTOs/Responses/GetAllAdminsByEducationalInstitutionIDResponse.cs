namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses;

public record GetAllAdminsByEducationalInstitutionIDResponse
{
    public ICollection<string> Admins { get; init; }
}