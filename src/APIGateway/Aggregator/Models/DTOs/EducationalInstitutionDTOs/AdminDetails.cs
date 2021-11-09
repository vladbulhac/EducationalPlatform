using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs;

public record AdminDetails
{
    public string Identity { get; init; }
    public ICollection<string> Permissions { get; init; }
}