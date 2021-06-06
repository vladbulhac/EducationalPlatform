using Microsoft.Extensions.Logging;
using Notification.API.Data;
using Notification.API.IntegrationEvents.Events;
using Notification.API.Repositories;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.API.IntegrationEvents.Handlers
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
                Event eventEntity = new()
                {
                    TriggeredByAction = @event.TriggeredBy.Action,
                    Name = @event.GetType().Name,
                    Url = @event.Url,
                    TimeIssued = @event.TimeIssued,
                    IssuedBy = @event.TriggeredBy.ServiceName,
                    Message = @event.Message
                };

                eventEntity.Recipients = new List<Recipient>(@event.ToNotify.Count);
                foreach (var person in @event.ToNotify)
                    eventEntity.Recipients.Add(new(person, eventEntity.EventID));

                await repository.CreateEvent(eventEntity);
            }
            catch (Exception e)
            {
                logger.LogError("Could not finish handling the following event: {0}, error details => {1}", @event, e.Message);
            }
        }
    }
}