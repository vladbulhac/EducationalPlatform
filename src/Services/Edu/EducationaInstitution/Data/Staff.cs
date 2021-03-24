using EducationaInstitutionAPI.Utils.Enums;
using System;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Defines the properties of a member from the Educational Institution's personnel
    /// </summary>
    /// <remarks>From IT personnel to deans and other important members that don't fulfill a teaching role in the institution</remarks>
    public class Staff
    {
        public Guid IdentityID { get; private set; }
        public Rank Rank { get; private set; }
        public EduInstitution EducationalInstitution { get; private set; }
        public Availability Availability { get; private set; }

        public Staff()
        {
        }

        public Staff(Rank rank, Guid identityID, EduInstitution eduInstitution)
        {
            Rank = rank;
            IdentityID = identityID;
            EducationalInstitution = eduInstitution;
            Availability = new();
        }
    }
}