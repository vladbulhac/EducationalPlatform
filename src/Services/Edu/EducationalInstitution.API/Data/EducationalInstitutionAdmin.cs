using EducationalInstitutionAPI.Data.Helpers;
using System;

namespace EducationalInstitutionAPI.Data
{
    public class EducationalInstitutionAdmin
    {
        public Guid AdminID { get; init; }
        public Guid EducationalInstitutionID { get; init; }
        public EducationalInstitution EducationalInstitution { get; init; }
        public Access EntityAccess { get; private set; }

        public EducationalInstitutionAdmin()
        {
        }

        public EducationalInstitutionAdmin(Guid adminID, Guid educationalInstitutionID)
        {
            AdminID = adminID;
            EducationalInstitutionID = educationalInstitutionID;
            EntityAccess = new();
        }
    }
}