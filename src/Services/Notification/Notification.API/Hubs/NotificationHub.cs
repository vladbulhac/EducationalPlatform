using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Notification.API.Hubs.DataTransferObjects;
using Notification.Application.DTOs;
using Notification.Application.Services;
using OpenIddict.Validation.AspNetCore;

namespace Notification.API.Hubs;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class NotificationHub : Hub<INotificationHub>
{
    private readonly ILogger<NotificationHub> logger;
    private readonly INotificationService notificationService;

    public NotificationHub(INotificationService notificationService, ILogger<NotificationHub> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    public async Task GetNotifications(GetNotificationsOfRecipientDTO dto)
    {
        var notifications = await notificationService.GetNotificationsAsync(Context.UserIdentifier, dto.Offset, dto.ResultsCount);

        await Clients.Caller.ReceiveNotifications(notifications);
    }

    public async Task SendNotifications(string recipient, ICollection<NotificationBody> notifications)
    {
        await Clients.User(recipient).ReceiveNotifications(notifications);
    }

    //todo add dto validation
    public async Task Seen(GetNotificationDTO dto)
    {
        await notificationService.NotificationSeenAsync(Context.UserIdentifier, dto.EventId);
    }

    public async Task SeenAll(GetNotificationsDTO dto)
    {
        await notificationService.NotificationSeenAsync(Context.UserIdentifier, dto.EventsIds);
    }

    public override async Task OnConnectedAsync()
    {
        logger.LogInformation("[SignalR.Hub] User {0} with connection {1} has connected",
                               Context.UserIdentifier,
                               Context.ConnectionId);

        await Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception ex)
    {
        logger.LogInformation("[SignalR.Hub] User {0} with connection {1} has been disconnected, error details => {2}",
                               Context.UserIdentifier,
                               Context.ConnectionId,
                               ex.Message);

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        await base.OnDisconnectedAsync(ex);
    }
}