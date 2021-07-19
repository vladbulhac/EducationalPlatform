using EducationalInstitution.Domain.Models;
using System;

namespace EducationalInstitution.Domain.Building_Blocks
{
    /// <summary>
    /// Represents an object that is defined by its identity and not by its attributes.
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; init; }

        public Access Access { get; protected set; }

        protected Entity()
        { }

        protected Entity(Guid? id)
        {
            if (id.HasValue && id == Guid.Empty) throw new ArgumentException($"{nameof(id)} doesn't have a valid value!");

            Id = id ?? Guid.NewGuid();
            Access = new();
        }

        public virtual void ScheduleForDeletion(double daysUntilDeletion = 30)
        {
            if (daysUntilDeletion <= 0) throw new ArgumentOutOfRangeException(nameof(daysUntilDeletion), "Value must be greater than 0!");

            DateTime currentDateAndTime = DateTime.UtcNow.Date;

            Access = new()
            {
                IsDisabled = true,
                DateForPermanentDeletion = currentDateAndTime.AddDays(daysUntilDeletion)
                                                             .ToUniversalTime()
            };
        }

        public void ClearDeletionDate() => Access = new() { IsDisabled = false, DateForPermanentDeletion = null };
    }
}