using EducationalInstitution.Application.Integration_Events;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using EducationalInstitution.Infrastructure.Repositories.Command_Repository.Results;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQEventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Commands.Handlers
{
    public class UpdateEducationalInstitutionCommandHandler : HandlerBase<UpdateEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<UpdateEducationalInstitutionCommand, Response>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;
        private readonly IEventBus eventBus;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public UpdateEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWork, IEventBus eventBus, ILogger<UpdateEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Tries to update the name and/or description of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// An <see cref="Response">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if the <see cref="EducationalInstitution"/> has not been found</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(UpdateEducationalInstitutionCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var commandResult = await UpdateEducationalInstitution(request, cancellationToken);
                    if (commandResult == default)
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    PublishNotificationEventsForAdmins(request.EducationalInstitutionID, commandResult.AdminsToNotify);
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
                    e.Message);
            }

            return new()
            {
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent,
                Message = string.Empty
            };
        }

        private async Task<AfterCommandChangesDetails> UpdateEducationalInstitution(UpdateEducationalInstitutionCommand request, CancellationToken cancellationToken)
        {
            switch (request.UpdateName)
            {
                case true when request.UpdateDescription:
                    return await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                           .UpdateNameAndDescriptionAsync(request.EducationalInstitutionID, request.Name, request.Description, cancellationToken);

                case false when request.UpdateDescription:
                    return await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                           .UpdateDescriptionAsync(request.EducationalInstitutionID, request.Description, cancellationToken);

                default:
                    return await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                           .UpdateNameAsync(request.EducationalInstitutionID, request.Name, cancellationToken);
            }
        }

        private static string GetNameOfRepositoryMethodThatWasCalledInHandler(UpdateEducationalInstitutionCommand request)
        {
            switch (request.UpdateName)
            {
                case true when request.UpdateDescription: return nameof(IEducationalInstitutionCommandRepository.UpdateNameAndDescriptionAsync);
                case false when request.UpdateDescription: return nameof(IEducationalInstitutionCommandRepository.UpdateDescriptionAsync);
                default: return nameof(IEducationalInstitutionCommandRepository.UpdateNameAsync);
            }
        }

        private void PublishNotificationEventsForAdmins(Guid educationalInstitutionID, ICollection<string> adminsToNotify)
        {
            NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @event = new()
            {
                Message = "An Educational Institution has been recently updated!",
                Uri = $"/edu/{educationalInstitutionID}",
                ToNotify = adminsToNotify,
                TriggeredBy = new()
                {
                    Action = "Update",
                    ServiceName = this.GetType().Namespace.Split('.')[0]
                }
            };

            eventBus.Publish(@event);
        }
    }
}