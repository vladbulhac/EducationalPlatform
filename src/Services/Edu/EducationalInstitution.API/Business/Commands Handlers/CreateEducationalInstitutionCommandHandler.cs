using EducationalInstitutionAPI.Business.IntegrationEvents_Handlers;
using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Events_Definitions;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.Command_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class CreateEducationalInstitutionCommandHandler : HandlerBase<CreateEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<DTOEducationalInstitutionCreateCommand, Response<EducationalInstitutionCommandResult>>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public CreateEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWork, IEventBus eventBus, ILogger<CreateEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Tries to create and save to the database a new <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">Created</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.MultiStatus">MultiStatus</see> if an ID for the <see cref="EducationalInstitution">Parent Institution</see> has provided but has not been found in the database but the <see cref="EducationalInstitution"/> has been saved in the database</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionCreateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    EducationalInstitution parentInstitution = null;
                    if (request.ParentInstitutionID != default)
                        parentInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                                   .GetEducationalInstitutionIncludingAdminsAsync(request.ParentInstitutionID, cancellationToken);

                    EducationalInstitution newEducationalInstitution = new(request.Name,
                                                                           request.Description,
                                                                           request.LocationID,
                                                                           request.BuildingsIDs,
                                                                           request.AdminsIDs,
                                                                           parentInstitution);

                    await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                            .CreateAsync(newEducationalInstitution, cancellationToken);
                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    PublishNotificationEventsToAdmins(newEducationalInstitution.EducationalInstitutionID,
                                                      request.AdminsIDs,
                                                      parentInstitution?.Admins.Select(a => a.AdminID).ToList());

                    if (parentInstitution is null && request.ParentInstitutionID != default)
                        return new()
                        {
                            Data = new() { EducationalInstitutionID = newEducationalInstitution.EducationalInstitutionID },
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.MultiStatus,
                            Message = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!"
                        };

                    return new()
                    {
                        Data = new() { EducationalInstitutionID = newEducationalInstitution.EducationalInstitutionID },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.Created,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                return HandleException<Response<EducationalInstitutionCommandResult>>(
                    error_message: "Could not create an Educational Institution with the request data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                    response_message: "An error occurred while creating the Educational Institution with the given data!",
                    JsonConvert.SerializeObject(request),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                    nameof(IEducationalInstitutionCommandRepository.CreateAsync),
                    e.Message);
            }
        }

        private void PublishNotificationEventsToAdmins(Guid newEntityID, ICollection<Guid> newEntityAdmins, ICollection<Guid> parentAdmins)
        {
            AssignedAdminsToEducationalInstitutionIntegrationEvent @newEntityEvent = new()
            {
                Message = "Admin rights granted for a recently created Educational Institution!",
                ToNotify = newEntityAdmins,
                Url = $"/edu/{newEntityID}",
                TriggeredBy = new()
                {
                    ServiceName = this.GetType().Namespace.Split('.')[0],
                    Action = "Create"
                }
            };
            eventBus.Publish(@newEntityEvent);

            if (parentAdmins is not null && parentAdmins.Count > 0)
            {
                NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent @newChildInstitutionEvent = new()
                {
                    Message = "An Educational Institution assigned this institution as a parent!",
                    ToNotify = parentAdmins,
                    Url = $"/edu/{newEntityID}",
                    TriggeredBy = new()
                    {
                        ServiceName = this.GetType().Namespace.Split('.')[0],
                        Action = "Create"
                    }
                };
                eventBus.Publish(@newChildInstitutionEvent);
            }
        }
    }
}