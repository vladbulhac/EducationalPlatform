using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using EducationalInstitutionAPI.Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class UpdateEducationalInstitutionLocationCommandHandler : IRequestHandler<DTOEducationalInstitutionLocationUpdateCommand, Response>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public UpdateEducationalInstitutionLocationCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to update the locationID and/or the BuildingsIDs of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    if (!await IsEducationalInstitutionUpdatedAndSavedToDatabase(request, cancellationToken))
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };

                    return new()
                    {
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.NoContent,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                   "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                   JsonConvert.SerializeObject(request),
                   unitOfWork.GetType(),
                   unitOfWork.UsingEducationalInstitutionRepository().GetType(),
                   GetNameOfRepositoryMethodThatWasCalledInHandler(request),
                   e.Message
                   );

                return new()
                {
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while updating the Educational Institution with the following ID: {request.EducationalInstitutionID}!"
                };
            }
        }

        private async Task<bool> IsEducationalInstitutionUpdatedAndSavedToDatabase(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken)
        {
            bool isEntityUpdated;
            switch (request.UpdateLocation)
            {
                case true when request.UpdateBuildings:
                    isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                .UpdateEntireLocationAsync(request.EducationalInstitutionID, request.LocationID, request.AddBuildingsIDs, request.RemoveBuildingsIDs, cancellationToken);
                    break;

                case false when request.UpdateBuildings:
                    isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                .UpdateBuildingsAsync(request.EducationalInstitutionID, request.AddBuildingsIDs, request.RemoveBuildingsIDs, cancellationToken);
                    break;

                default:
                    isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                .UpdateLocationAsync(request.EducationalInstitutionID, request.LocationID, cancellationToken);
                    break;
            }

            if (isEntityUpdated)
                await unitOfWork.SaveChangesAsync(cancellationToken);

            return isEntityUpdated;
        }

        private static string GetNameOfRepositoryMethodThatWasCalledInHandler(DTOEducationalInstitutionLocationUpdateCommand request)
        {
            switch (request.UpdateLocation)
            {
                case true when request.UpdateBuildings: return nameof(IEducationalInstitutionRepository.UpdateEntireLocationAsync);
                case false when request.UpdateBuildings: return nameof(IEducationalInstitutionRepository.UpdateBuildingsAsync);
                default: return nameof(IEducationalInstitutionRepository.UpdateLocationAsync);
            }
        }
    }
}