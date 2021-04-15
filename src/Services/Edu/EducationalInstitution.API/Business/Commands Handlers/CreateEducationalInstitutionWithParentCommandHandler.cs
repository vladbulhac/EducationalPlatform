using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
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
    /// Defines a method that handles the creation of an <see cref="EducationalInstitution"/> entity that has a parent institution
    /// </summary>
    public class CreateEducationalInstitutionWithParentCommandHandler : IRequestHandler<DTOEducationalInstitutionWithParentCreateCommand, Response<EducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<CreateEducationalInstitutionWithParentCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        public CreateEducationalInstitutionWithParentCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEducationalInstitutionWithParentCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to create and save to the database a new <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="request">Contains <see cref="EducationalInstitution"/> data that is to be added to the database</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">if operation is successful</see></item>
        /// <item><see cref="HttpStatusCode.MultiStatus">if the <see cref="EducationalInstitution">Parent Institution</see> has not been found</see></item>
        /// <item><see cref="HttpStatusCode.InternalServerError">if the entity could not be inserted into the database</see></item>
        /// </list>
        /// </returns>
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionWithParentCreateCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    var parentInstitution = await unitOfWork.UsingEducationalInstitutionRepository()
                                                            .GetEntityByIDAsync(request.ParentInstitutionID, cancellationToken);

                    EducationalInstitution newEducationalInstitution = new(
                                                                            request.Name,
                                                                            request.Description,
                                                                            request.LocationID,
                                                                            request.BuildingsIDs,
                                                                            parentInstitution
                                                                            );

                    await unitOfWork.UsingEducationalInstitutionRepository()
                                            .CreateAsync(newEducationalInstitution, cancellationToken);
                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    if (parentInstitution is null)
                        return new()
                        {
                            ResponseObject = new() { EduInstitutionID = newEducationalInstitution.EducationalInstitutionID },
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.MultiStatus,
                            Message = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!"
                        };
                    else
                        return new()
                        {
                            ResponseObject = new() { EduInstitutionID = newEducationalInstitution.EducationalInstitutionID },
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.Created,
                            Message = string.Empty
                        };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not create an Educational Institution with the request data: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                    JsonConvert.SerializeObject(request),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionRepository().GetType(),
                    nameof(IEducationalInstitutionRepository.CreateAsync),
                    e.Message
                    );

                return new()
                {
                    ResponseObject = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while creating the Educational Institution with the given data!"
                };
            }
        }
    }
}