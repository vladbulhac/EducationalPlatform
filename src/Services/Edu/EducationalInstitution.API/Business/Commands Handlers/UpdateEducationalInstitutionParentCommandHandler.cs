using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Events_Definitions;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
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
    public class UpdateEducationalInstitutionParentCommandHandler : HandlerBase<UpdateEducationalInstitutionParentCommandHandler>,
                                                                    IRequestHandler<DTOEducationalInstitutionParentUpdateCommand, Response>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;
        private readonly IEventBus eventBus;

        /// <exception cref="ArgumentNullException"/>
        public UpdateEducationalInstitutionParentCommandHandler(IUnitOfWorkForCommands unitOfWork, IEventBus eventBus, ILogger<UpdateEducationalInstitutionParentCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Tries to update the ParentInstitution field of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided EducationalInstitutionID or ParentInstitutionID</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(DTOEducationalInstitutionParentUpdateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var commandResult = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                               .UpdateParentInstitutionAsync(request.EducationalInstitutionID, request.ParentInstitutionID, cancellationToken);

                    if (commandResult == default)
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"The Educational Institution with the ID: {request.EducationalInstitutionID} or the Parent Educational Institution with the ID: {request.ParentInstitutionID} has not been found!"
                        };

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @event = new()
                    {
                        Message = "An Educational Institution's parent has been recently updated!",
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
                        error_message: "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3} and {4}, error details => {5}",
                        response_message: "An error occurred while updating the parent of the Educational Institution with the given data!",
                        JsonConvert.SerializeObject(request),
                        unitOfWork.GetType(),
                        unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                        nameof(IEducationalInstitutionQueryRepository.GetEntityByIDAsync),
                        nameof(IEducationalInstitutionCommandRepository.UpdateParentInstitutionAsync),
                        e.Message);
            }

            return new()
            {
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent,
                Message = string.Empty
            };
        }
    }
}