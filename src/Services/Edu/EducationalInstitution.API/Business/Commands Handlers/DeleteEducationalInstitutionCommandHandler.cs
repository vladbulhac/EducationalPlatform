using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Commands_Handlers
{
    public class DeleteEducationalInstitutionCommandHandler : IRequestHandler<DTOEducationalInstitutionDeleteCommand, Response<DeleteEducationalInstitutionCommandResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<DeleteEducationalInstitutionCommandHandler> logger;

        private readonly IUnitOfWorkForCommands unitOfWorkCommand;
        private readonly IUnitOfWorkForQueries unitOfWorkQuery;

        /// <exception cref="ArgumentNullException"/>
        public DeleteEducationalInstitutionCommandHandler(IUnitOfWorkForCommands unitOfWorkCommand, IUnitOfWorkForQueries unitOfWorkQuery, ILogger<DeleteEducationalInstitutionCommandHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWorkCommand = unitOfWorkCommand ?? throw new ArgumentNullException(nameof(unitOfWorkCommand));
            this.unitOfWorkQuery = unitOfWorkQuery ?? throw new ArgumentNullException(nameof(unitOfWorkQuery));
        }

        /// <summary>
        /// Schedules an <see cref="EducationalInstitution"/> entity for deletion
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.Accepted">Accepted</see> if the entity has been scheduled for deletion</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if the <see cref="EducationalInstitution"/> has not been found</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the schedule for deletion could not be saved in the database</item>
        /// </list>
        /// </returns>
        public async Task<Response<DeleteEducationalInstitutionCommandResult>> Handle(DTOEducationalInstitutionDeleteCommand request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                using (unitOfWorkCommand)
                {
                    var educationalInstitution = await unitOfWorkQuery.UsingEducationalInstitutionQueryRepository()
                                                                 .GetEntityByIDAsync(request.EducationalInstitutionID, cancellationToken);

                    if (educationalInstitution is null)
                    {
                        return new()
                        {
                            Data = null,
                            OperationStatus = false,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
                        };
                    }

                    educationalInstitution.EntityAccess.ScheduleForDeletion();
                    await unitOfWorkCommand.SaveChangesAsync(cancellationToken);

                    return new()
                    {
                        Data = new() { DateForPermanentDeletion = educationalInstitution.EntityAccess.DateForPermanentDeletion.Value.ToUniversalTime() },
                        OperationStatus = true,
                        StatusCode = HttpStatusCode.Accepted,
                        Message = string.Empty
                    };
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not schedule for deletion the Educational Institution with ID: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                    request.EducationalInstitutionID,
                    unitOfWorkCommand.GetType(),
                    unitOfWorkCommand.UsingEducationalInstitutionCommandRepository().GetType(),
                    nameof(IEducationalInstitutionQueryRepository.GetEntityByIDAsync),
                    e.Message
                    );

                return new()
                {
                    Data = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while scheduling for deletion the Educational Institution with the following ID: {request.EducationalInstitutionID}!"
                };
            }
        }
    }
}