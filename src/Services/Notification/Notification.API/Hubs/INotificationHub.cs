using Notification.API.Hubs.Management;
using Notification.Application.DTOs;

namespace Notification.API.Hubs;

public interface INotificationHub : IHubCall
{
    public Task ReceiveNotification(NotificationBody notification);

    public Task ReceiveNotifications(ICollection<NotificationBody> notifications);
}