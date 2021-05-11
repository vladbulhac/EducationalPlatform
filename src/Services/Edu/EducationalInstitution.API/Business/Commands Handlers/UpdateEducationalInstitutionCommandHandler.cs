using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class UpdateEducationalInstitutionCommandHandler : HandlerBase<UpdateEducationalInstitutionCommandHandler>,
                                                              IRequestHandler<DTOEducationalInstitutionUpdateCommand, Response>
    {
        private readonly IUnitOfWorkForCommands unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public UpdateEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWork, ILogger<UpdateEducationalInstitutionCommandHandler> logger) : base(logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to update the name and/or description of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(DTOEducationalInstitutionUpdateCommand request, CancellationToken cancellationToken = default)
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
                return HandleException<Response>(
                    error_message: "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                    response_message: "An error occurred while updating the Educational Institution with the following ID: {0}!",
                    JsonConvert.SerializeObject(request),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionCommandRepository().GetType(),
                    GetNameOfRepositoryMethodThatWasCalledInHandler(request),
                    e.Message
                    );
            }
        }

        private async Task<bool> IsEducationalInstitutionUpdatedAndSavedToDatabase(DTOEducationalInstitutionUpdateCommand request, CancellationToken cancellationToken)
        {
            bool isEntityUpdated;
            switch (request.UpdateName)
            {
                case true when request.UpdateDescription:
                    isEntityUpdated = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                        .UpdateNameAndDescriptionAsync(request.EducationalInstitutionID, request.Name, request.Description, cancellationToken);
                    break;

                case false when request.UpdateDescription:
                    isEntityUpdated = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                         .UpdateDescriptionAsync(request.EducationalInstitutionID, request.Description, cancellationToken);
                    break;

                default:
                    isEntityUpdated = await unitOfWork.UsingEducationalInstitutionCommandRepository()
                                                        .UpdateNameAsync(request.EducationalInstitutionID, request.Name, cancellationToken);
                    break;
            }

            if (isEntityUpdated)
                await unitOfWork.SaveChangesAsync(cancellationToken);

            return isEntityUpdated;
        }

        private static string GetNameOfRepositoryMethodThatWasCalledInHandler(DTOEducationalInstitutionUpdateCommand request)
        {
            switch (request.UpdateName)
            {
                case true when request.UpdateDescription: return nameof(IEducationalInstitutionCommandRepository.UpdateNameAndDescriptionAsync);
                case false when request.UpdateDescription: return nameof(IEducationalInstitutionCommandRepository.UpdateDescriptionAsync);
                default: return nameof(IEducationalInstitutionCommandRepository.UpdateNameAsync);
            }
        }
    }
}