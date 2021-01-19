using Identity.API.Utils.Enums;
using System;
using System.Globalization;

namespace Identity.API.Models
{
    public class Student
    {
        public string Id { get; private set; }
        public Year Year { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string SchoolId { get; private set; }

        #region Set Methods

        public static Student Create(Year year, string startDate, string endDate, string schoolId)
        {
            return new Student
            {
                Id = Guid.NewGuid().ToString(),
                Year = year,
                SchoolId = schoolId,
                StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }

        public void SetInfo(Year year = default, string startDate = null, string endDate = null, string schoolId = null)
        {
            Year = year == Year.NotSet ? Year : year;
            StartDate = startDate == null ? StartDate : DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            EndDate = endDate == null ? EndDate : DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            SchoolId = schoolId == null ? SchoolId : schoolId;
        }

        #endregion Set Methods
    }
}