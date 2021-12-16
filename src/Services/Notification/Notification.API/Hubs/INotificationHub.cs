using Notification.Application.DTOs;

namespace Notification.API.Hubs;

public interface INotificationHub
{
    public Task ReceiveNotification(NotificationBody notification);

    public Task ReceiveNotifications(ICollection<NotificationBody> notifications);
}