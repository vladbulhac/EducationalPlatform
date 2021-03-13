using EducationaInstitutionAPI.Utils.Enums;
using System;
using System.Globalization;

namespace EducationaInstitutionAPI.Data.Helpers
{
    public class InstitutionAttended
    {
        public Guid InstitutionAttendedID { get; init; }
        public EduInstitution EducationalInstitution { get; private set; }
        public Year? StartYear { get; private set; }
        public Year? EndYear { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public InstitutionAttended(EduInstitution eduInstitution, string startDate, string endDate, Year? startYear = null, Year? endYear = null)
        {
            InstitutionAttendedID = Guid.NewGuid();
            EducationalInstitution = eduInstitution;
            StartYear = startYear;
            EndYear = endYear;
            StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public InstitutionAttended()
        {
        }

        public void UpdateDates(string startDate, string endDate)
        {
            StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public void UpdateYears(Year startYear, Year endYear)
        {
            StartYear = startYear;
            EndYear = endYear;
        }
    }
}