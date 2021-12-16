using Microsoft.AspNetCore.SignalR;

namespace Notification.API.Hubs.Management;

public class ResourceOwnerIdentification : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("sub")?.Value;
    }
}