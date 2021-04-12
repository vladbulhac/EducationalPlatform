using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Unit_of_Work;
using EducationaInstitutionAPI.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Business.Commands_Handlers.EducationalInstitution_Commands
{
    /// <summary>
    /// Defines a method that handles the update of an <see cref="EduInstitution"/>'s location and buildings
    /// </summary>
    public class UpdateEducationalInstitutionLocationCommandHandler : IRequestHandler<DTOEducationalInstitutionLocationUpdateCommand, Response<EducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public UpdateEducationalInstitutionLocationCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateEducationalInstitutionLocationCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to update the locationID and/or the BuildingsIDs of an <see cref="EduInstitution"/> entity
        /// </summary>
        /// <param name="request">Contains <see cref="EduInstitution.LocationID"/> and/or <see cref="EduInstitution.Buildings"/></param>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.NotFound">if no <see cref="EduInstitution"/> has been found for the provided id</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be updated</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionLocationUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    bool isEntityUpdated = false;
                    if (request.UpdateBuildings && request.UpdateLocation)
                        isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .UpdateAsync(request.EduInstitutionID, request.LocationID, request.BuildingsIDs, cancellationToken);
                    else
                    if (request.UpdateLocation)
                        isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .UpdateAsync(request.EduInstitutionID, request.LocationID, cancellationToken);
                    else
                        isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .UpdateAsync(request.EduInstitutionID, request.BuildingsIDs, cancellationToken);

                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    if (!isEntityUpdated)
                        return new()
                        {
                            ResponseObject = null,
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EduInstitutionID} has not been found!"
                        };

                    return new()
                    {
                        ResponseObject = new() { EduInstitutionID = request.EduInstitutionID },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not update the Educational Institution with ID: {0}, using {1}'s method: {2} with {3}, {4}, error details => {5}",
                    request.EduInstitutionID, unitOfWork.GetType(), nameof(IEducationalInstitutionRepository.UpdateAsync), nameof(request.LocationID), nameof(request.BuildingsIDs), e.Message
                    );

                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while updating the Educational Institution with the following ID: {request.EduInstitutionID}!"
                };
            }
        }
    }
}