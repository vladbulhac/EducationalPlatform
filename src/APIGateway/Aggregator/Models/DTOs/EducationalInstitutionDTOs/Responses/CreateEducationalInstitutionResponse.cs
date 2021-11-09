using System;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses;

public record CreateEducationalInstitutionResponse
{
    public Guid EducationalInstitutionID { get; init; }
}