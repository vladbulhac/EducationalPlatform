using Notification.Application.DTOs;

namespace Notification.Application.Services;

/// <summary>
/// Handles notification related actions.
/// </summary>
public interface INotificationService
{
    public Task<ICollection<NotificationBody>> GetNotificationsAsync(string userId, int offset, int resultsCount);

    public Task NotificationSeenAsync(string userId, string eventId);

    public Task NotificationSeenAsync(string userId, ICollection<string> eventsIds);
}