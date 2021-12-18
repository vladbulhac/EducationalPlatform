using Notification.API.Hubs.DataTransferObjects;

namespace Notification.API.Hubs.Management;

public interface IHubCall
{
    public Task CallFailed(HubCallFailDetails details);
}