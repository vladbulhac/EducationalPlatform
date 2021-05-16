using System;

namespace Notification.API.Data
{
    public record PublicEvent
    {
        public DateTime TimeIssued { get; init; }
        public string Message { get; init; }
        public string Url { get; init; }
    }
}