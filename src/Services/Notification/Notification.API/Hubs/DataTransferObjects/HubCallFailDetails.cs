using System.Net;

namespace Notification.API.Hubs.DataTransferObjects;

public record HubCallFailDetails
{
    public HttpStatusCode StatusCode { get; init; }
    public string Message { get; init; }

    public HubCallFailDetails(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message ?? string.Empty;
    }
}