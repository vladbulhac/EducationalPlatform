using System;
using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests
{
    public class DTOUpdateEducationalInstitutionAdminRequest
    {
        public ICollection<Guid> AddAdminsIDs { get; init; }
        public ICollection<Guid> RemoveAdminsIDs { get; init; }
    }
}