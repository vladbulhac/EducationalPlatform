using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Events_Definitions;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.Command_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.Abstractions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class DeleteEducationalInstitutionCommandHandler : HandlerBase<DeleteEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<DTOEducationalInstitutionDeleteCommand, Response<DeleteEducationalInstitutionCommandResult>>
    {
        private readonly IUnitOfWorkForCommands unitOfWorkCommand;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public DeleteEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, IEventBus eventBus, ILogger<DeleteEducationalInstitutionCommandHandler> logger) : base(logger)
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
        public async Task<Response<DeleteEducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionDeleteCommand request, CancellationToken cancellationToken = default)
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

                    NotifyAdminsOfEducationalInstitutionDeletionScheduledDateIntegrationEvent @event = new()
                    {
                        Message = $"The Educational Institution with ID: {request.EducationalInstitutionID} has been scheduled for deletion on: {commandResult.ScheduledDateForDeletion}!",
                        ToNotify = commandResult.AdminsToNotify,
                        Url = string.Empty,
                        TriggeredBy = new()
                        {
                            ServiceName = this.GetType().Namespace.Split('.')[0],
                            Action = "Delete"
                        }
                    };

                    eventBus.Publish(@event);

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
                return HandleException<Response<DeleteEducationalInstitutionCommandResult>>(
                                         error_message: "Could not schedule for deletion the Educational Institution with ID: {0}, using {1} and {2} with {3}, error details => {4}",
                                         response_message: "An error occurred while scheduling for deletion the Educational Institution with the following ID: {0}!",
                                        request.EducationalInstitutionID,
                                        unitOfWorkCommand.GetType(),
                                        unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                                        nameof(IEducationalInstitutionCommandRepository.ScheduleForDeletionAsync),
                                        e.Message
                                        );
            }
        }
    }
}