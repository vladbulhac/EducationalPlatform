using System;
using System.Collections.Generic;

namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Requests
{
    public class DTOUpdateEducationalInstitutionAdminRequest
    {
        public ICollection<string> AddAdminsIDs { get; init; }
        public ICollection<string> RemoveAdminsIDs { get; init; }
    }
}