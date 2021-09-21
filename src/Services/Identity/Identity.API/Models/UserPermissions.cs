using System.Collections.Generic;

namespace Identity.API.Models
{
    public class UserPermissions
    {
        public string UserId { get; init; }
        public User User { get; init; }
        public ICollection<EducationalInstitutionAdministrator> EducationalInstitutionAdminPermissions { get; set; }
    }
}