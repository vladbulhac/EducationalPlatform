using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses;

public class GetAllAdminsByEducationalInstitutionIDResponse
{
    public ICollection<string> Admins { get; init; }
}