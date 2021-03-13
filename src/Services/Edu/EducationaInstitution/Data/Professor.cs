using EducationaInstitutionAPI.Data.Helpers;
using EducationaInstitutionAPI.Utils.Enums;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.Data
{
    public class Professor
    {
        public Guid IdentityID { get; private set; }
        public Rank Rank { get; private set; }
        public string OfficeID { get; private set; }
        public ICollection<InstitutionAttended> InstitutionsAttended { get; private set; }
        public Availability Availability { get; private set; }

        public Professor(Guid identityID, Rank rank, string startDate, IList<EduInstitution> eduInstitutions, string officeID)
        {
            IdentityID = identityID;
            Rank = rank;
            OfficeID = officeID;
            InstitutionsAttended = SetTheInstitutionsAttendedHistory(eduInstitutions, startDate);
            Availability = new();
        }

        public Professor()
        {
        }

        public void SetOffice(string officeid)
        {
            OfficeID = officeid;
        }

        private static ICollection<InstitutionAttended> SetTheInstitutionsAttendedHistory(IList<EduInstitution> eduInstitutions, string startDate)
        {
            List<InstitutionAttended> institutionsAttended = new(eduInstitutions.Count);
            for (int i = 0; i < eduInstitutions.Count; i++)
            {
                institutionsAttended.Add(new(eduInstitutions[i], startDate, startDate));
            }

            return institutionsAttended;
        }
    }
}