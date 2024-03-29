﻿using Notification.Domain.Building_Blocks;
using Notification.Domain.Models.Aggregates;

namespace Notification.Domain.Models;

/// <summary>
/// A person or thing that receives a notification
/// </summary>
public class Recipient : Entity
{
    public string EventId { get; init; }
    public Event Event { get; init; }
    public bool Seen { get; private set; }

    public Recipient()
    { }

    public Recipient(string personId, string eventId) : base(personId)
    {
        EventId = eventId;
        Seen = false;
    }

    public void ChangeSeenStatus(bool newStatus) => Seen = newStatus;
}