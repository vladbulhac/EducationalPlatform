using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Query_Repository;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Business.Queries_Handlers
{
    public class GetAllEducationalInstitutionsByNameQueryHandler : IRequestHandler<DTOEducationalInstitutionsByNameQuery, Response<GetAllEducationalInstitutionsByNameQueryResult>>
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<GetAllEducationalInstitutionsByNameQueryHandler> logger;

        private readonly IUnitOfWorkForQueries unitOfWork;

        /// <exception cref="ArgumentNullException"/>
        public GetAllEducationalInstitutionsByNameQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetAllEducationalInstitutionsByNameQueryHandler> logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tries to get one or more <see cref="EducationalInstitution"/>s whose Name's contain a given string
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if the entity could not be inserted into the database</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<GetAllEducationalInstitutionsByNameQueryResult>> Handle(DTOEducationalInstitutionsByNameQuery request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                var educationalInstitutions = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                                        .GetAllLikeNameAsync(request.Name, request.OffsetValue, request.ResultsCount, cancellationToken);

                if (educationalInstitutions is null || educationalInstitutions.Count == 0)
                    return new()
                    {
                        Data = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Could not find any Educational Institution with a name like: {request.Name}!"
                    };

                return new()
                {
                    Data = new() { EducationalInstitutions = educationalInstitutions },
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                logger.LogError(
                    "Could not find any Educational Institution with the given data: {0}, using {1} and {2}'s method: {3}, error details => {4}",
                    JsonConvert.SerializeObject(request),
                    unitOfWork.GetType(),
                    unitOfWork.UsingEducationalInstitutionQueryRepository().GetType(),
                    nameof(IEducationalInstitutionQueryRepository.GetAllLikeNameAsync),
                    e.Message
                    );

                return new()
                {
                    Data = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while searching for any Educational Institution with a name like: {request.Name}!"
                };
            }
        }
    }
}