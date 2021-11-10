using Notification.Domain.Building_Blocks;

namespace Notification.Domain.Models;

public record TriggerDetails : ValueObject
{
    public string Action { get; init; }
    public string Issuer { get; init; }
    public DateTime TimeIssued { get; init; }

    public TriggerDetails(string action, string issuer, DateTime timeIssued)
    {
        Action = action ?? throw new ArgumentNullException(nameof(action));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));

        TimeIssued = timeIssued;
    }
}