using Microsoft.Extensions.Logging;
using Notification.Application.Integration_Events.Events;
using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories;
using RabbitMQEventBus.IntegrationEvents;

namespace Notification.Application.Integration_Events.Handlers;

public class AssignedAdminsToEducationalInstitutionIntegrationEventHandler : IIntegrationEventHandler<AssignedAdminsToEducationalInstitutionIntegrationEvent>
{
    private readonly INotificationRepository repository;
    private readonly ILogger<AssignedAdminsToEducationalInstitutionIntegrationEventHandler> logger;

    public AssignedAdminsToEducationalInstitutionIntegrationEventHandler(INotificationRepository repository, ILogger<AssignedAdminsToEducationalInstitutionIntegrationEventHandler> logger)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleEvent(AssignedAdminsToEducationalInstitutionIntegrationEvent @event)
    {
        logger.LogInformation($"Received integration event: {@event} !");

        try
        {
            foreach (var adminDetails in @event.NewAdmins)
            {
                Event eventEntity = new(@event.GetType().Name,
                                        adminDetails.DetailedMessage,
                                        @event.Uri,
                                        @event.TimeIssued,
                                        @event.TriggeredBy.Action,
                                        @event.TriggeredBy.ServiceName,
                                        adminDetails.Identity,
                                        @event.EventId);

                await repository.AddAsync(eventEntity);
                await repository.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogError("Could not finish handling the following event: {0}, error details => {1}", @event, e.Message);
        }
    }
}