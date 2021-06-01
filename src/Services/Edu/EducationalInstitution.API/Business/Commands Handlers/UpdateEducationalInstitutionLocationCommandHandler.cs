using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Events_Definitions;
using EducationalInstitutionAPI.Data.Repository_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.Command_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
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
    public class UpdateEducationalInstitutionLocationCommandHandler : HandlerBase<UpdateEducationalInstitutionLocationCommandHandler>,
                                                                      IRequestHandler<DTOEducationalInstitutionLocationUpdateCommand, Response>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public UpdateEducationalInstitutionLocationCommandHandler(IUnitOfWorkForCommands unitOfWork, IEventBus eventBus, ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Tries to update the LocationID and/or the BuildingsIDs of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var commandResult = await IsEducationalInstitutionUpdatedAndSavedToDatabase(request, cancellationToken);

                    if (commandResult == default)
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @event = new()
                    {
                        Message = "An Educational Institution's location has been recently updated!",
                        TriggeredBy = new()
                        {
                            Action = "Update",
                            ServiceName = this.GetType().Namespace.Split('.')[0]
                        },
                        Url = $"/edu/{request.EducationalInstitutionID}",
                        ToNotify = commandResult.AdminsToNotify,
                    };
                    eventBus.Publish(@event);
                }
            }
            catch (Exception e)
            {
                return HandleException<Response>(
                            error_message: "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                            response_message: "An error occurred while updating the Educational Institution with the given data!",
                            JsonConvert.SerializeObject(request),
                            unitOfWork.GetType(),
                            unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                            GetNameOfRepositoryMethodThatWasCalledInHandler(request),
                            e.Message
                            );
            }

            return new()
            {
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent,
                Message = string.Empty
            };
        }

        private async Task<CommandRepositoryResult> IsEducationalInstitutionUpdatedAndSavedToDatabase(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken)
        {
            switch (request.UpdateLocation)
            {
                case true when request.UpdateBuildings:
                    return await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                .UpdateEntireLocationAsync(request.EducationalInstitutionID, request.LocationID, request.AddBuildingsIDs, request.RemoveBuildingsIDs, cancellationToken);

                case false when request.UpdateBuildings:
                    return await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                .UpdateBuildingsAsync(request.EducationalInstitutionID, request.AddBuildingsIDs, request.RemoveBuildingsIDs, cancellationToken);

                default:
                    return await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                .UpdateLocationAsync(request.EducationalInstitutionID, request.LocationID, cancellationToken);
            }
        }

        private static string GetNameOfRepositoryMethodThatWasCalledInHandler(DTOEducationalInstitutionLocationUpdateCommand request)
        {
            switch (request.UpdateLocation)
            {
                case true when request.UpdateBuildings: return nameof(IEducationalInstitutionCommandRepository.UpdateEntireLocationAsync);
                case false when request.UpdateBuildings: return nameof(IEducationalInstitutionCommandRepository.UpdateBuildingsAsync);
                default: return nameof(IEducationalInstitutionCommandRepository.UpdateLocationAsync);
            }
        }
    }
}