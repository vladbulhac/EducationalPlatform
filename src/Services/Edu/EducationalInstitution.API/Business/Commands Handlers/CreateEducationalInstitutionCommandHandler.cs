using EducationalInstitutionAPI.Business.IntegrationEvents_Handlers;
using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class CreateEducationalInstitutionCommandHandler : HandlerBase<CreateEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<DTOEducationalInstitutionCreateCommand, Response<EducationalInstitutionCommandResult>>
    {
        private readonly IUnitOfWorkForCommands unitOfWorkCommand;
        private readonly IUnitOfWorkForQueries unitOfWorkQuery;
        private readonly IEventBus eventBus;

        /// <exception cref="ArgumentNullException"/>
        public CreateEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, IUnitOfWorkForQueries unitOfWorkQuery, IEventBus eventBus, ILogger<CreateEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWorkCommand = unitOfWorkCommand ?? throw new ArgumentNullException(nameof(unitOfWorkCommand));
            this.unitOfWorkQuery = unitOfWorkQuery ?? throw new ArgumentNullException(nameof(unitOfWorkQuery));
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
                using (unitOfWorkCommand)
                {
                    EducationalInstitution parentInstitution = null;
                    if (request.ParentInstitutionID != default)
                        parentInstitution = await unitOfWorkQuery.UsingEducationalInstitutionQueryRepository()
                                                            .GetEntityByIDAsync(request.ParentInstitutionID, cancellationToken);

                    EducationalInstitution newEducationalInstitution = new(
                                                                            request.Name,
                                                                            request.Description,
                                                                            request.LocationID,
                                                                            request.BuildingsIDs,
                                                                            request.AdminsIDs,
                                                                            parentInstitution
                                                                            );

                    await unitOfWorkCommand.UsingEducationalInstitutionCommandRepository()
                                            .CreateAsync(newEducationalInstitution, cancellationToken);
                    await unitOfWorkCommand.SaveChangesAsync(cancellationToken);

                    if (parentInstitution is null && request.ParentInstitutionID != default)
                        return new()
                        {
                            Data = new() { EducationalInstitutionID = newEducationalInstitution.EducationalInstitutionID },
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.MultiStatus,
                            Message = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!"
                        };

                    AssignedAdminsToEducationalInstitutionIntegrationEvent @event = new()
                    {
                        Message = "You were given admin rights for an Educational Institution recently created!",
                        ToNotify = request.AdminsIDs,
                        Url = $"/edu/{newEducationalInstitution.EducationalInstitutionID}",
                        TriggeredBy = new()
                        {
                            ServiceName = this.GetType().Namespace.Split('.')[0],
                            Action = "Create"
                        }
                    };

                    eventBus.Publish(@event);

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
                    error_message: "Could not create an Educational Institution with the request data: {0}, using {1} with {2}'s method: {3} and {4} with {5}'s method: {6}, error details => {7}",
                     response_message: "An error occurred while creating the Educational Institution with the given data!",
                     JsonConvert.SerializeObject(request),
                    unitOfWorkCommand.GetType(),
                    unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                    nameof(IEducationalInstitutionCommandRepository.CreateAsync),
                    unitOfWorkQuery.GetType(),
                    unitOfWorkQuery.UsingEducationalInstitutionQueryRepository().GetType(),
                    nameof(IEducationalInstitutionQueryRepository.GetEntityByIDAsync),
                    e.Message
                    );
            }
        }
    }
}