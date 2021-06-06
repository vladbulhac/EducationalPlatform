using System;
using System.Collections.Generic;

namespace Aggregator.DTOs.EducationalInstitutionDTOs.Responses
{
    public class GetAllAdminsByEducationalInstitutionIDResponse
    {
        public ICollection<Guid> Admins { get; init; }
    }
}