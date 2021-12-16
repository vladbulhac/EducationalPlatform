using Microsoft.Extensions.Logging;
using Notification.Application.DTOs;
using Notification.Infrastructure.Repositories;

namespace Notification.Application.Services;

/// <summary>
/// <inheritdoc cref="INotificationService"/>
/// </summary>
public class NotificationService : INotificationService
{
    private readonly INotificationRepository repository;
    private readonly ILogger<NotificationService> logger;

    public NotificationService(INotificationRepository repository, ILogger<NotificationService> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ICollection<NotificationBody>> GetNotificationsAsync(string userId, int offset, int resultsCount)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentException($"{nameof(userId)} is null or empty!");
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (resultsCount < 0) throw new ArgumentOutOfRangeException(nameof(resultsCount));

        try
        {
            using (repository)
            {
                var @events = await repository.GetAsync(userId, offset, resultsCount);

                return events.Select(e => new NotificationBody(e, userId)).ToArray();
            }
        }
        catch (Exception e)
        {
            logger.LogError("Could not fetch any new notification for user {0}, error details=> {1}", userId, e.Message);
            return new List<NotificationBody>(0);
        }
    }

    public async Task NotificationSeenAsync(string userId, string eventId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
        if (string.IsNullOrEmpty(eventId)) throw new ArgumentNullException(nameof(eventId));

        try
        {
            using (repository)
            {
                var @event = await repository.GetByIdAsync(eventId, userId);

                if (@event is not null)
                    @event.RecipientSawEventNotification(userId);

                await repository.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogError("Could not update seen status of notification with eventId {0} and recipientId {1}, error details=> {2}", eventId, userId, e.Message);
        }
    }

    public async Task NotificationSeenAsync(string userId, ICollection<string> eventsIds)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
        if (eventsIds is null || eventsIds.Count == 0) throw new ArgumentException(nameof(eventsIds));

        using (repository)
        {
            try
            {
                var @events = await repository.GetEventsFromCollectionAsync(eventsIds, userId);

                foreach (var @event in @events)
                {
                    try
                    {
                        @event.RecipientSawEventNotification(userId);
                        await repository.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        logger.LogError("Could not update seen status of notification with eventId {0} and recipientId {1}, error details=> {2}", @event.Id, userId, e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError("Could not fetch the notifications of recipient {0}, error details=> {1}", userId, e.Message);
            }
        }
    }
}