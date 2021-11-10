using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories.Results;

namespace Notification.Infrastructure.Repositories;

public interface INotificationRepository : IRepository<Event>
{
    public Task<ICollection<GetEventDetails>> GetUnseenEventsForRecipientAsync(string recipientID, CancellationToken cancellationToken = default);
}