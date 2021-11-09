using System;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests;

public record DTOUpdateEducationalInstitutionParentRequest
{
    public Guid ParentInstitutionID { get; init; }
}