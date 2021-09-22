using EducationalInstitution.Application.Commands.Results;
using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Commands.Handlers
{
    public class DisableEducationalInstitutionCommandHandler : HandlerBase<DisableEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<DisableEducationalInstitutionCommand, Response<DisableEducationalInstitutionCommandResult>>
    {
        private readonly IUnitOfWorkForCommands unitOfWorkCommand;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public DisableEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, IEventBus eventBus, ILogger<DisableEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWorkCommand = unitOfWorkCommand ?? throw new ArgumentNullException(nameof(unitOfWorkCommand));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Schedules an <see cref="EducationalInstitution"/> entity for deletion
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Accepted">Accepted</see> if the entity has been scheduled for deletion</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if the <see cref="EducationalInstitution"/> has not been found</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the schedule for deletion could not be saved in the database</item>
        /// </list>
        /// </returns>
        public async Task<Response<DisableEducationalInstitutionCommandResult>> Handle(DisableEducationalInstitutionCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWorkCommand)
                {
                    var commandResult = await unitOfWorkCommand.UsingEducationalInstitutionCommandRepository()
                                                               .ScheduleForDeletionAsync(request.EducationalInstitutionID, cancellationToken);
                    if (commandResult == default)
                        return new()
                        {
                            Data = null,
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };

                    await unitOfWorkCommand.SaveChangesAsync(cancellationToken);

                    PublishNotificationEventsForAdmins(request.EducationalInstitutionID,
                                                       commandResult.ScheduledDateForDeletion,
                                                       commandResult.AdminsToNotify);

                    return new()
                    {
                        Data = new() { DateForPermanentDeletion = commandResult.ScheduledDateForDeletion },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.Accepted,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                return HandleException<Response<DisableEducationalInstitutionCommandResult>>(
                         error_message: "Could not schedule for deletion the Educational Institution with ID: {0}, using {1} and {2} with {3}, error details => {4}",
                         response_message: "An error occurred while scheduling for deletion the Educational Institution with the following ID: {0}!",
                         request.EducationalInstitutionID,
                         unitOfWorkCommand.GetType(),
                         unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                         nameof(IEducationalInstitutionCommandRepository.ScheduleForDeletionAsync),
                         e.Message);
            }
        }

        private void PublishNotificationEventsForAdmins(Guid educationalInstitutionID, DateTime scheduledDateForDeletion, ICollection<string> adminsToNotify)
        {
            NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent @event = new()
            {
                Message = $"The Educational Institution with ID: {educationalInstitutionID} has been scheduled for deletion on: {scheduledDateForDeletion}!",
                ToNotify = adminsToNotify,
                Uri = string.Empty,
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Delete"
                }
            };

            eventBus.Publish(@event);
        }
    }
}