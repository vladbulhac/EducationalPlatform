using EducationaInstitutionAPI.Utils.Enums;
using System;

namespace EducationaInstitutionAPI.Data
{
    public class Staff
    {
        public Guid IdentityID { get; private set; }
        public Rank Rank { get; private set; }
        public EduInstitution EducationalInstitution { get; private set; }
        public Availability Availability { get; private set; }

        public Staff(Rank rank, Guid identityID, EduInstitution eduInstitution)
        {
            Rank = rank;
            IdentityID = identityID;
            EducationalInstitution = eduInstitution;
            Availability = new();
        }
        public Staff() { }
    }
}