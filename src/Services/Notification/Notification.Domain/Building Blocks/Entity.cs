using System;

namespace Notification.Domain.Building_Blocks
{
    /// <summary>
    /// Represents an object that is defined by its identity and not by its attributes.
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; init; }

        protected Entity()
        { }

        protected Entity(Guid? id)
        {
            if (id.HasValue && id == Guid.Empty) throw new ArgumentException($"{nameof(id)} doesn't have a valid value!");

            Id = id ?? Guid.NewGuid();
        }
    }
}