using System;
using System.Collections.Generic;

namespace Notification.API.Data
{
    public class Event
    {
        public Guid EventID { get; private set; }
        public DateTime TimeIssued { get; init; }
        public string Message { get; init; }
        public string TriggeredByAction { get; init; }
        public string Url { get; init; }
        public ICollection<Recipient> Recipients { get; set; }
        public string IssuedBy { get; init; }

        public Event()
        {
            EventID = Guid.NewGuid();
        }
    }
}