namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs;

public record struct AdminDetails
{
    public string Identity { get; init; }
    public ICollection<string> Permissions { get; init; }
}