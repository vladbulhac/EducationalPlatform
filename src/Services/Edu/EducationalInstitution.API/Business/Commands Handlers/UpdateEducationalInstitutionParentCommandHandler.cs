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
    public class UpdateEducationalInstitutionParentCommandHandler : IRequestHandler<DTOEducationalInstitutionParentUpdateCommand, Response>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<UpdateEducationalInstitutionParentCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public UpdateEducationalInstitutionParentCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateEducationalInstitutionParentCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to update the ParentInstitution field of an <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.NoContent">NoContent</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided ParentInstitutionID</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided EducationalInstitutionID</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be updated</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response> Handle(DTOEducationalInstitutionParentUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var parentInstitution = await unitOfWork.UsingEducationalInstitutionRepository()
                                                           .GetEntityByIDAsync(request.ParentInstitutionID, cancellationToken);

                    if (parentInstitution is null)
                        return new()
                        {
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"The Parent Educational Institution with the following ID: {request.ParentInstitutionID} has not been found!"
                        };

                    var isEntityUpdated = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .UpdateParentInstitutionAsync(request.EducationalInstitutionID, parentInstitution, cancellationToken);

                    if (!isEntityUpdated)
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
                   "Could not update the Educational Institution with the given data: {0}, using {1} with {2}'s method: {3} and {4}, error details => {5}",
                   JsonConvert.SerializeObject(request),
                   unitOfWork.GetType(),
                   unitOfWork.UsingEducationalInstitutionRepository().GetType(),
                   nameof(IEducationalInstitutionRepository.GetEntityByIDAsync),
                   nameof(IEducationalInstitutionRepository.UpdateParentInstitutionAsync),
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
    }
}