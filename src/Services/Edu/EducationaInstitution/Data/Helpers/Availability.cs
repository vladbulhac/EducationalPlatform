using EducationaInstitutionAPI.Utils;
using System;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Defines if an entity can be returned as a request response or it must be removed from the database based on the ScheduledForPermanentDeletion's value
    /// </summary>
    /// <remarks>All entities that are to be removed and include an object of this class will remain in the database for a determined amount of time in case the deletion operation must be reverted</remarks>
    public class Availability
    {
        /// <value>True if the entity is scheduled for deletion, False otherwise</value>
        public bool IsDisabled { get; private set; }

        /// <summary>
        /// Describes the date at which the entity is completely removed from the database
        /// </summary>
        /// <value>DateTime object if IsDisabled is True or NULL value if IsDisabled is False</value>
        public DateTime? DateForPermanentDeletion { get; private set; }

        public Availability()
        {
            IsDisabled = false;
            DateForPermanentDeletion = null;
        }

        /// <summary>
        /// Sets the IsDisabled field and generates a date when the entity will be removed from the database
        /// </summary>
        /// <remarks>A "DaysUntilDeletion" property must be declared in the appsettings.json</remarks>
        public void ScheduleForDeletion()
        {
            IsDisabled = true;

            DateTime currentDateAndTime = new();
            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            DateForPermanentDeletion = currentDateAndTime.AddDays(int.Parse(daysFromConfigFile));
        }

        /// <summary>
        /// Sets the IsDisabled field and clears the DateForPermanentDeletion object
        /// </summary>
        public void RemoveDeletionSchedule()
        {
            IsDisabled = false;
            DateForPermanentDeletion = null;
        }
    }
}