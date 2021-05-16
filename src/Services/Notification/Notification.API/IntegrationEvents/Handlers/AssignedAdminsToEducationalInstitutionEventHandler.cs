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
    public class AssignedAdminsToEducationalInstitutionEventHandler : IIntegrationEventHandler<AssignedAdminsToEducationalInstitutionIntegrationEvent>
    {
        private readonly INotificationRepository repository;
        private readonly ILogger<AssignedAdminsToEducationalInstitutionEventHandler> logger;

        public AssignedAdminsToEducationalInstitutionEventHandler(ILogger<AssignedAdminsToEducationalInstitutionEventHandler> logger, INotificationRepository repository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleEvent(AssignedAdminsToEducationalInstitutionIntegrationEvent @event)
        {
            logger.LogInformation($"Received integration event: {@event} !");

            try
            {
                Event eventEntity = new()
                {
                    TriggeredByAction = @event.TriggeredByAction,
                    Url = @event.Url,
                    TimeIssued = @event.TimeIssued,
                    IssuedBy = @event.TriggeredByService_Name,
                    Message = @event.Message
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