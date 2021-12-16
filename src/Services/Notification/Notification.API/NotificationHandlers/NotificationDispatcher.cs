using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notification.API.Hubs;
using Notification.Application.DTOs;

namespace Notification.API.NotificationHandlers;

public class NotificationDispatcher : INotificationHandler<NotificationBody>
{
    private readonly ILogger<NotificationDispatcher> logger;
    private readonly IHubContext<NotificationHub, INotificationHub> hub;

    public NotificationDispatcher(IHubContext<NotificationHub, INotificationHub> hub, ILogger<NotificationDispatcher> logger)
    {
        this.hub = hub ?? throw new ArgumentNullException(nameof(hub));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(NotificationBody notification, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Sending message {0} to {1}.", notification.Title, notification.To);

        await hub.Clients.Group(notification.To).ReceiveNotification(notification);
    }
}