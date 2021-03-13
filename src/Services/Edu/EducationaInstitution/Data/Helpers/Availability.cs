using System;

namespace EducationaInstitutionAPI.Data
{
    public class Availability
    {
        public bool IsDisabled { get; private set; }
        public DateTime? ScheduledForPermanentDeletion { get; private set; }

        public Availability()
        {
            IsDisabled = false;
            ScheduledForPermanentDeletion = null;
        }

        public void ScheduleForDeletion()
        {
            IsDisabled = true;

            DateTime currentDateAndTime = new();
            ScheduledForPermanentDeletion = currentDateAndTime.AddDays(30);
        }
    }
}