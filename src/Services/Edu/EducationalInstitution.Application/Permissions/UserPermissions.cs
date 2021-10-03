using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Permissions
{
    public record UserPermissions
    {
        public const string All = "user.educational_institution.all";
        public const string UpdateDetails = "user.educational_institution.update_details";
        public const string ChangeAdministrators = "user.educational_institution.change_administrators";
        public const string Delete = "user.educational_institution.delete";
    }
}