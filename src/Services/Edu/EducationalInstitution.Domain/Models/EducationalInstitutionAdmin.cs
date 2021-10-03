using EducationalInstitution.Domain.Building_Blocks;
using System;
using System.Collections.Generic;
using Model = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Domain.Models
{
    public class EducationalInstitutionAdmin : StringEntity
    {
        public Guid EducationalInstitutionId { get; init; }
        public Model::EducationalInstitution EducationalInstitution { get; init; }
        public ICollection<string> Permissions { get; private set; }

        public EducationalInstitutionAdmin()
        {
        }

        public EducationalInstitutionAdmin(string adminId, Guid educationalInstitutionId, ICollection<string> permissions) : base(adminId)
        {
            EducationalInstitutionId = educationalInstitutionId;

            if (permissions is null) throw new ArgumentNullException($"{nameof(permissions)} is null or empty!");

            Permissions = new HashSet<string>(permissions);
        }

        public void RevokePermissions(ICollection<string> revokedPermissions)
        {
            foreach (var permission in revokedPermissions)
                Permissions.Remove(permission);
        }

        public void GrantPermissions(ICollection<string> grantedPermissions)
        {
            foreach (var permission in grantedPermissions)
                Permissions.Add(permission);
        }
    }
}