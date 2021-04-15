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
    /// <summary>
    /// Defines a method that handles the update of an <see cref="EducationalInstitution"/>'s name and/or description
    /// </summary>
    public class UpdateEducationalInstitutionCommandHandler : IRequestHandler<DTOEducationalInstitutionUpdateCommand, Response>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<UpdateEducationalInstitutionCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public UpdateEducationalInstitutionCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateEducationalInstitutionCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response> Handle(DTOEducationalInstitutionUpdateCommand request, CancellationToken cancellationToken)
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
                    else
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

        private async Task<bool> IsEducationalInstitutionUpdatedAndSavedToDatabase(DTOEducationalInstitutionUpdateCommand request, CancellationToken cancellationToken)
        {
            bool isEntityUpdated;
            if (request.UpdateName && request.UpdateDescription)
                isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                    .UpdateNameDescriptionAsync(request.EducationalInstitutionID, request.Name, request.Description, cancellationToken);
            else if (request.UpdateName)
                isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                    .UpdateNameAsync(request.EducationalInstitutionID, request.Name, cancellationToken);
            else
                isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                    .UpdateDescriptionAsync(request.EducationalInstitutionID, request.Description, cancellationToken);

            if (isEntityUpdated)
                await unitOfWork.SaveChangesAsync(cancellationToken);

            return isEntityUpdated;
        }

        private static string GetNameOfRepositoryMethodThatWasCalledInHandler(DTOEducationalInstitutionUpdateCommand request)
        {
            if (request.UpdateName && request.UpdateDescription)
                return nameof(IEducationalInstitutionRepository.UpdateNameDescriptionAsync);
            else
            if (request.UpdateName)
                return nameof(IEducationalInstitutionRepository.UpdateNameAsync);
            else
                return nameof(IEducationalInstitutionRepository.UpdateDescriptionAsync);
        }
    }
}