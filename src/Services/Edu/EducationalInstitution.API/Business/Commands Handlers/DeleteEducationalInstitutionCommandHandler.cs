﻿using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Events_Definitions;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.Abstractions;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class DeleteEducationalInstitutionCommandHandler : HandlerBase<DeleteEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<DTOEducationalInstitutionDeleteCommand, Response<DeleteEducationalInstitutionCommandResult>>
    {
        private readonly IUnitOfWorkForCommands unitOfWorkCommand;
        private readonly IUnitOfWorkForQueries unitOfWorkQuery;
        private readonly IEventBus eventBus;

        /// <exception cref="ArgumentNullException"/>
        public DeleteEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, IUnitOfWorkForQueries unitOfWorkQuery, IEventBus eventBus, ILogger<DeleteEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWorkCommand = unitOfWorkCommand ?? throw new ArgumentNullException(nameof(unitOfWorkCommand));
            this.unitOfWorkQuery = unitOfWorkQuery ?? throw new ArgumentNullException(nameof(unitOfWorkQuery));
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
                    var educationalInstitution = await unitOfWorkQuery.UsingEducationalInstitutionQueryRepository()
                                                                 .GetEntityByIDAsync(request.EducationalInstitutionID, cancellationToken);

                    if (educationalInstitution is null)
                    {
                        return new()
                        {
                            Data = null,
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };
                    }

                    educationalInstitution.EntityAccess.ScheduleForDeletion();
                    await unitOfWorkCommand.SaveChangesAsync(cancellationToken);

                    NotifyAdminsOfEducationalInstitutionDeletionScheduledDateIntegrationEvent @event = new()
                    {
                        Message = $"The Educational Institution with ID: {educationalInstitution.EducationalInstitutionID} has been scheduled for deletion on: {educationalInstitution.EntityAccess.DateForPermanentDeletion.Value}!",
                        ToNotify = educationalInstitution.Admins.Select(a => a.AdminID).ToList(),
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
                        Data = new() { DateForPermanentDeletion = educationalInstitution.EntityAccess.DateForPermanentDeletion.Value.ToUniversalTime() },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.Accepted,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                return HandleException<Response<DeleteEducationalInstitutionCommandResult>>(
                                         error_message: "Could not schedule for deletion the Educational Institution with ID: {0}, using {1} with {2} and {3} with {4}'s method: {5}, error details => {6}",
                                         response_message: "An error occurred while scheduling for deletion the Educational Institution with the following ID: {0}!",
                                        request.EducationalInstitutionID,
                                        unitOfWorkCommand.GetType(),
                                        unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                                        unitOfWorkQuery.GetType(),
                                        unitOfWorkQuery.UsingEducationalInstitutionQueryRepository().GetType(),
                                        nameof(IEducationalInstitutionQueryRepository.GetEntityByIDAsync),
                                        e.Message
                                        );
            }
        }
    }
}