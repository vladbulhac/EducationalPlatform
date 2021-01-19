using Identity.API.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Identity.API.Models
{
    public class Professor
    {
        public string Id { get; set; }
        public Rank Rank { get; set; }
        public DateTime StartDate { get; set; }
        public string OfficeId { get; set; }
        public ICollection<string> SchoolsIds { get; set; }

        #region Set Methods

        public static Professor Create(Rank rank, string startDate, string officeId, ICollection<string> schoolsIds)
        {
            return new Professor
            {
                Id = Guid.NewGuid().ToString(),
                Rank = rank,
                StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                OfficeId = officeId,
                SchoolsIds = new HashSet<string>(schoolsIds)
            };
        }

        public void SetInfo(Rank rank = default, string startDate = null, string officeId = null, ICollection<string> schoolsIds = default)
        {
            Rank = rank == Rank.NotSet ? Rank : rank;
            StartDate = startDate == null ? StartDate : DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            OfficeId = officeId == null ? OfficeId : officeId;
            SchoolsIds = schoolsIds.Count == 0 ? SchoolsIds : new HashSet<string>(schoolsIds);
        }

        #endregion Set Methods
    }
}