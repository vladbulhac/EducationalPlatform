using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Application.DTOs;
using Notification.Application.Integration_Events.Events;
using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories;
using RabbitMQEventBus.IntegrationEvents;

namespace Notification.Application.Integration_Events.Handlers;

public class AssignedAdminsToEducationalInstitutionIntegrationEventHandler : IIntegrationEventHandler<AssignedAdminsToEducationalInstitutionIntegrationEvent>
{
    private readonly IMediator mediator;
    private readonly INotificationRepository repository;
    private readonly ILogger<AssignedAdminsToEducationalInstitutionIntegrationEventHandler> logger;

    public AssignedAdminsToEducationalInstitutionIntegrationEventHandler(INotificationRepository repository, IMediator mediator, ILogger<AssignedAdminsToEducationalInstitutionIntegrationEventHandler> logger)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Saves to the database the received event and sends it as notifications to <see cref="INotificationHandler{TNotification}"/>.
    /// </summary>
    public async Task HandleEvent(AssignedAdminsToEducationalInstitutionIntegrationEvent @event)
    {
        logger.LogInformation($"Received integration event: {@event} !");

        try
        {
            var notifications = new List<NotificationBody>(@event.NewAdmins.Count);

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

                notifications.Add(new NotificationBody(eventEntity, adminDetails.Identity));

                await repository.AddAsync(eventEntity);
                await repository.SaveChangesAsync();
            }

            foreach (var notification in notifications)
                await mediator.Publish(notification);
        }
        catch (Exception e)
        {
            logger.LogError("Could not finish handling the following event: {0}, error details => {1}", @event, e.Message);
        }
    }
}