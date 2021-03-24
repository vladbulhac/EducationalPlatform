using EducationaInstitutionAPI.Data.Helpers;
using EducationaInstitutionAPI.Utils.Enums;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Defines the properties of a Student
    /// </summary>
    public class Student
    {
        public Guid IdentityID { get; private set; }
        public Year CurrentYear { get; private set; }
        public ICollection<InstitutionAttended> InstitutionsAttended { get; private set; }
        public Availability Availability { get; private set; }

        public Student(Guid identityID, string startDate, string endDate, EduInstitution eduInstitution, Year year)
        {
            IdentityID = identityID;
            CurrentYear = year;
            InstitutionsAttended = new List<InstitutionAttended>() { new(eduInstitution, startDate, endDate, year) };
            Availability = new();
        }

        public Student()
        {
        }

        public void UpdateIdentity(Guid identityID)
        {
            IdentityID = identityID;
        }
    }
}