using System;

namespace Notification.API.Data
{
    /// <summary>
    /// A person or thing that receives a notification
    /// </summary>
    public class Recipient
    {
        public Guid RecipientID { get; init; }
        public Guid EventID { get; init; }
        public Event Event { get; init; }
        public bool Seen { get; private set; }

        public Recipient()
        {
            Seen = false;
        }

        public Recipient(Guid personID, Guid eventID) : this()
        {
            RecipientID = personID;
            EventID = eventID;
        }

        public bool NotificationWasSeen() => Seen = true;
    }
}