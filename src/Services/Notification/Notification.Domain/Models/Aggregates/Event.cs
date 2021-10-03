using Notification.Domain.Building_Blocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notification.Domain.Models.Aggregates
{
    public class Event : Entity, IAggregateRoot
    {
        public string Name { get; init; }
        public string Message { get; init; }
        public string Uri { get; init; }
        public TriggerDetails TriggerDetails { get; init; }
        public ICollection<Recipient> Recipients { get; private set; }

        public Event()
        {
        }

        private Event(string name, string message, string uri, DateTime timeIssued, string triggeredByAction, string issuedBy, string id = null) : base(id)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : GetEventName(name);
            Message = message ?? Name;
            Uri = uri ?? "NO_URI_SPECIFIED";
            TriggerDetails = new(triggeredByAction, issuedBy, timeIssued);

            Recipients = new HashSet<Recipient>();
        }

        public Event(string name, string message, string uri, DateTime timeIssued, string triggeredByAction, string issuedBy, ICollection<string> recipients, string id = null) : this(name, message, uri, timeIssued, triggeredByAction, issuedBy, id)
        {
            AttachRecipientsToEvent(recipients);
        }

        public Event(string name, string message, string uri, DateTime timeIssued, string triggeredByAction, string issuedBy, string recipientId, string id = null) : this(name, message, uri, timeIssued, triggeredByAction, issuedBy, id)
        {
            AddRecipient(recipientId);
        }

        private static string GetEventName(string @eventName) => eventName.Substring(0, eventName.Length - "IntegrationEvent".Length);

        public static Event ReconstituteEvent(string id, string name, string message, string uri, DateTime timeIssued, string triggeredByAction, string issuedBy, ICollection<Recipient> recipients)
        {
            return new()
            {
                Id = id,
                Name = name,
                Message = message,
                Uri = uri,
                TriggerDetails = new(triggeredByAction, issuedBy, timeIssued),
                Recipients = recipients
            };
        }

        private void AddRecipient(string recipientId)
        {
            Recipients.Add(new Recipient(recipientId, Id));
        }

        private void AttachRecipientsToEvent(ICollection<string> recipients)
        {
            if (recipients is null || recipients.Count == 0) throw new ArgumentException($"Parameter { nameof(recipients) } can't be empty!");

            foreach (var recipient in recipients)
                AddRecipient(recipient);
        }

        public void RecipientSawEventNotification(string recipientId)
        {
            var recipient = Recipients.SingleOrDefault(r => r.Id == recipientId);
            if (recipient == default) throw new KeyNotFoundException($"{recipientId} does not exist in event - {Id} collection!");

            recipient.ChangeSeenStatus(true);
        }
    }
}