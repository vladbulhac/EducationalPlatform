using System;

namespace Identity.API.Models
{
    public class EducationalInstitutionAdministrator
    {
        public string UserId { get; init; }
        public User User { get; init; }
        public Guid EducationalInstitutionId { get; set; }

        public bool CanChangeAdministrators { get; set; }
        public bool CanUpdateEducationalInstitutionDetails { get; set; }
        public bool CanRemoveEducationalInstitution { get; set; }
    }
}