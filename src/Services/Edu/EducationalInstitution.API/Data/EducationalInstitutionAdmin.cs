using System;

namespace EducationalInstitutionAPI.Data
{
    public class EducationalInstitutionAdmin : Access
    {
        public Guid AdminID { get; init; }
        public Guid EducationalInstitutionID { get; init; }
        public EducationalInstitution EducationalInstitution { get; init; }

        public EducationalInstitutionAdmin()
        {
        }

        public EducationalInstitutionAdmin(Guid adminID, Guid educationalInstitutionID)
        {
            AdminID = adminID;
            EducationalInstitutionID = educationalInstitutionID;
        }
    }
}