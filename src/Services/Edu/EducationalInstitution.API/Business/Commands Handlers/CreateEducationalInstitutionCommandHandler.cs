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
    public class CreateEducationalInstitutionCommandHandler : IRequestHandler<DTOEducationalInstitutionCreateCommand, Response<EducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<CreateEducationalInstitutionCommandHandler> logger;

        private readonly IUnitOfWork unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public CreateEducationalInstitutionCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEducationalInstitutionCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Tries to create and save to the database a new <see cref="EducationalInstitution"/> entity
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Created">Created</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.MultiStatus">MultiStatus</see> if an ID for the <see cref="EducationalInstitution">Parent Institution</see> has provided but has not been found in the database but the <see cref="EducationalInstitution"/> has been saved in the database</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionCreateCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWork)
                {
                    EducationalInstitution parentInstitution = null;
                    if (request.ParentInstitutionID != default)
                        parentInstitution = await unitOfWork.UsingEducationalInstitutionRepository()
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

                    if (parentInstitution is null && request.ParentInstitutionID != default)
                        return new()
                        {
                            Data = new() { EducationalInstitutionID = newEducationalInstitution.EducationalInstitutionID },
                            OperationStatus = true,
                            StatusCode = HttpStatusCode.MultiStatus,
                            Message = $"The Educational Institution has been successfully created but the Parent Institution with the following ID: {request.ParentInstitutionID} has not been found!"
                        };

                    return new()
                    {
                        Data = new() { EducationalInstitutionID = newEducationalInstitution.EducationalInstitutionID },
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
                    Data = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while creating the Educational Institution with the given data!"
                };
            }
        }
    }
}