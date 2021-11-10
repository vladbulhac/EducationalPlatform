namespace Notification.Infrastructure.Repositories.Results
{
    public record GetEventDetails
    {
        public DateTime TimeIssued { get; init; }
        public string Message { get; init; }
        public string Url { get; init; }
    }
}