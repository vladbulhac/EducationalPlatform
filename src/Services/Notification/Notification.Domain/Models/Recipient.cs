using Notification.Domain.Building_Blocks;
using Notification.Domain.Models.Aggregates;
using System;

namespace Notification.Domain.Models
{
    /// <summary>
    /// A person or thing that receives a notification
    /// </summary>
    public class Recipient : Entity
    {
        public Guid EventID { get; init; }
        public Event Event { get; init; }
        public bool Seen { get; private set; }

        public Recipient()
        { }

        public Recipient(Guid personID, Guid eventID) : base(personID)
        {
            EventID = eventID;
            Seen = false;
        }

        public void ChangeSeenStatus(bool newStatus) => Seen = newStatus;
    }
}