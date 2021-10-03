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
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Application.Commands.Handlers
{
    public class UpdateEducationalInstitutionLocationCommandHandler : HandlerBase<UpdateEducationalInstitutionLocationCommandHandler>,
                                                                      IRequestHandler<UpdateEducationalInstitutionLocationCommand, Response>
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
        public async Task<Response> Handle(UpdateEducationalInstitutionLocationCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var educationalInstitution = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                                 .GetEducationalInstitutionIncludingAdminsAndBuildingsAsync(request.EducationalInstitutionID, cancellationToken);

                    if (educationalInstitution == default)
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };

                    UpdateLocationOfEducationalInstitution(request, educationalInstitution);

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    PublishNotificationEventsForAdmins(request.EducationalInstitutionID, educationalInstitution.Admins.Select(a => a.Id).ToList());
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
                            nameof(IEducationalInstitutionCommandRepository.GetEducationalInstitutionIncludingAdminsAndBuildingsAsync),
                            e.Message);
            }

            return new()
            {
                OperationStatus = true,
                StatusCode = HttpStatusCode.NoContent,
                Message = string.Empty
            };
        }

        private void UpdateLocationOfEducationalInstitution(UpdateEducationalInstitutionLocationCommand request, Domain::EducationalInstitution educationalInstitution)
        {
            switch (request.UpdateLocation)
            {
                case true when request.UpdateBuildings is true:
                    educationalInstitution.SetEntireLocation(request.LocationID, request.AddBuildingsIDs, request.RemoveBuildingsIDs);
                    break;

                case false when request.UpdateBuildings is true:
                    educationalInstitution.CreateAndAddBuildings(request.AddBuildingsIDs);
                    educationalInstitution.RemoveBuildings(request.RemoveBuildingsIDs);
                    break;

                default:
                    educationalInstitution.SetLocation(request.LocationID);
                    break;
            }
        }

        private void PublishNotificationEventsForAdmins(Guid educationalInstitutionID, ICollection<string> adminsToNotify)
        {
            NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent @event = new()
            {
                Message = "An Educational Institution's location has been recently updated!",
                TriggeredBy = new()
                {
                    Action = "Update",
                    ServiceName = this.GetType().Namespace.Split('.')[0]
                },
                Uri = $"/edu/{educationalInstitutionID}",
                ToNotify = adminsToNotify,
            };

            eventBus.Publish(@event);
        }
    }
}