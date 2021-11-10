namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests;

public record struct DTOUpdateEducationalInstitutionAdminRequest
{
    public ICollection<AdminDetails> NewAdmins { get; init; }
    public ICollection<AdminDetails> AdminsWithNewPermissions { get; init; }
    public ICollection<AdminDetails> AdminsWithRevokedPermissions { get; init; }
}