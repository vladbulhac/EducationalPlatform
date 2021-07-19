using EducationalInstitution.Infrastructure.Repositories.Query_Repository;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Queries.Handlers
{
    public class GetAllEducationalInstitutionsByNameQueryHandler : HandlerBase<GetAllEducationalInstitutionsByNameQueryHandler>,
                                                                   IRequestHandler<GetAllEducationalInstitutionsByNameQuery, Response<GetAllEducationalInstitutionsByNameQueryResult>>
    {
        private readonly IUnitOfWorkForQueries unitOfWork;

        /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
        public GetAllEducationalInstitutionsByNameQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetAllEducationalInstitutionsByNameQueryHandler> logger) : base(logger)
            => this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        /// <summary>
        /// Tries to get one or more <see cref="EducationalInstitution"/>s whose Name's contain a given string
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>
        /// An <see cref="Response{TData}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if an exception has been caught</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<GetAllEducationalInstitutionsByNameQueryResult>> Handle(GetAllEducationalInstitutionsByNameQuery request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                var queryResult = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                                  .GetAllByNameAsync(request.Name, request.OffsetValue, request.ResultsCount, cancellationToken);

                if (queryResult == default || queryResult.EducationalInstitutions.Count == 0)
                    return new()
                    {
                        Data = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Could not find any Educational Institution with a name like: {request.Name}!"
                    };

                return new()
                {
                    Data = queryResult,
                    OperationStatus = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                return HandleException<Response<GetAllEducationalInstitutionsByNameQueryResult>>(
                                error_message: "Could not find any Educational Institution with the given data: {0}, using {1} and {2}'s method: {3}, error details => {4}",
                                response_message: "An error occurred while searching for any Educational Institution with a name like: {5}!",
                                JsonConvert.SerializeObject(request),
                                unitOfWork.GetType(),
                                unitOfWork.UsingEducationalInstitutionQueryRepository().GetType(),
                                nameof(IEducationalInstitutionQueryRepository.GetAllByNameAsync),
                                e.Message,
                                request.Name);
            }
        }
    }
}