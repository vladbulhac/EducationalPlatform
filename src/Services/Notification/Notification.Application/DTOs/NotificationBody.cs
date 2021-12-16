using MediatR;
using Notification.Application.Extensions;
using Notification.Domain.Models.Aggregates;

namespace Notification.Application.DTOs;

public class NotificationBody : INotification
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string From { get; init; }
    public string To { get; init; }
    public string Message { get; init; }
    public DateTime IssuedAt { get; init; }
    public string Uri { get; init; }
    public bool Seen { get; init; }

    public NotificationBody(Event @event, string to)
    {
        Id = @event.Id;
        Title = @event.Name.AddSpacesAfterWords();

        From = @event.TriggerDetails.Issuer;
        To = to ?? throw new ArgumentNullException(nameof(to));

        Message = @event.Message;
        Uri = @event.Uri;

        IssuedAt = @event.TriggerDetails.TimeIssued;

        Seen = @event.Recipients.First(r => r.Id == to).Seen;
    }
}