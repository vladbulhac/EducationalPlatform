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
    public class NotifyAdminsOfEducationalInstitutionUpdateEventHandler : IIntegrationEventHandler<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent>
    {
        private readonly INotificationRepository repository;
        private readonly ILogger<NotifyAdminsOfEducationalInstitutionUpdateEventHandler> logger;

        public NotifyAdminsOfEducationalInstitutionUpdateEventHandler(INotificationRepository repository, ILogger<NotifyAdminsOfEducationalInstitutionUpdateEventHandler> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleEvent(NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @event)
        {
            logger.LogInformation($"Received integration event: {@event} !");

            try
            {
                Event eventEntity = new()
                {
                    Message = @event.Message,
                    IssuedBy = @event.TriggeredBy.ServiceName,
                    TimeIssued = @event.TimeIssued,
                    Url = @event.Url,
                    TriggeredByAction = @event.TriggeredBy.Action
                };

                eventEntity.Recipients = new List<Recipient>(@event.ToNotify.Count);
                foreach (var person in @event.ToNotify)
                    eventEntity.Recipients.Add(new(person, eventEntity.EventID));

                await repository.CreateEvent(eventEntity);
            }
            catch (Exception e)
            {
                logger.LogError("Could not finish handling the following event: {0}, error details=>{1}", @event, e.Message);
            }
        }
    }
}