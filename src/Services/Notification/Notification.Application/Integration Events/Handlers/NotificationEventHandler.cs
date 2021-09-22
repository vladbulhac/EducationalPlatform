using Microsoft.Extensions.Logging;
using Notification.Application.Integration_Events.Events;
using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Repositories;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Threading.Tasks;

namespace Notification.Application.Integration_Events.Handlers
{
    public class NotificationEventHandler<TEvent> : IIntegrationEventHandler<TEvent> where TEvent : NotificationIntegrationEvent
    {
        private readonly INotificationRepository repository;
        private readonly ILogger<NotificationEventHandler<TEvent>> logger;

        public NotificationEventHandler(INotificationRepository repository, ILogger<NotificationEventHandler<TEvent>> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleEvent(TEvent @event)
        {
            logger.LogInformation($"Received integration event: {@event} !");

            try
            {
                Event eventEntity = new(@event.GetType().Name,
                                        @event.Message, @event.Uri,
                                        @event.TimeIssued,
                                        @event.TriggeredBy.Action,
                                        @event.TriggeredBy.ServiceName,
                                        @event.ToNotify,
                                        @event.EventId);

                await repository.AddEventAsync(eventEntity);
            }
            catch (Exception e)
            {
                logger.LogError("Could not finish handling the following event: {0}, error details => {1}", @event, e.Message);
            }
        }
    }
}