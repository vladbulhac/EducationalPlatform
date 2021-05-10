using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
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

        private readonly IUnitOfWorkForCommands unitOfWorkCommand;
        private readonly IUnitOfWorkForQueries unitOfWorkQuery;

        /// <exception cref="ArgumentNullException"/>
        public CreateEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, IUnitOfWorkForQueries unitOfWorkQuery, ILogger<CreateEducationalInstitutionCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWorkCommand = unitOfWorkCommand ?? throw new ArgumentNullException(nameof(unitOfWorkCommand));
            this.unitOfWorkQuery = unitOfWorkQuery ?? throw new ArgumentNullException(nameof(unitOfWorkQuery));
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
        public async Task<Response<EducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionCreateCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWorkCommand)
                {
                    EducationalInstitution parentInstitution = null;
                    if (request.ParentInstitutionID != default)
                        parentInstitution = await unitOfWorkQuery.UsingEducationalInstitutionQueryRepository()
                                                            .GetEntityByIDAsync(request.ParentInstitutionID, cancellationToken);

                    EducationalInstitution newEducationalInstitution = new(
                                                                            request.Name,
                                                                            request.Description,
                                                                            request.LocationID,
                                                                            request.BuildingsIDs,
                                                                            request.AdminsIDs,
                                                                            parentInstitution
                                                                            );

                    await unitOfWorkCommand.UsingEducationalInstitutionCommandRepository()
                                            .CreateAsync(newEducationalInstitution, cancellationToken);
                    await unitOfWorkCommand.SaveChangesAsync(cancellationToken);

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
                    unitOfWorkCommand.GetType(),
                    unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                    nameof(IEducationalInstitutionCommandRepository.CreateAsync),
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