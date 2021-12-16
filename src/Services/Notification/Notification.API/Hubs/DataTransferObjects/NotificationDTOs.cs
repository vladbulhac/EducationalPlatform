namespace Notification.API.Hubs.DataTransferObjects;

public record GetNotificationDTO
{
    public string EventId { get; init; }
}

public record GetNotificationsDTO
{
    public ICollection<string> EventsIds { get; init; }
}

public record GetNotificationsOfRecipientDTO
{
    public int Offset { get; init; }
    public int ResultsCount { get; init; }
}