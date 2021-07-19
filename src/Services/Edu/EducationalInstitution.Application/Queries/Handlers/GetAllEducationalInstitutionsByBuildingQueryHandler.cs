using EducationalInstitution.Infrastructure.Repositories.Query_Repository;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Queries.Handlers
{
    public class GetAllEducationalInstitutionsByBuildingQueryHandler : HandlerBase<GetAllEducationalInstitutionsByBuildingQueryHandler>,
                                                                       IRequestHandler<GetAllEducationalInstitutionsByBuildingQuery, Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>>
    {
        private readonly IUnitOfWorkForQueries unitOfWork;

        public GetAllEducationalInstitutionsByBuildingQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetAllEducationalInstitutionsByBuildingQueryHandler> logger) : base(logger)
            => this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        /// <summary>
        /// Tries to get a collection of <see cref="EducationalInstitution"/>s by BuildingID
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation _________</param>
        /// <returns>
        /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
        /// <list type="bullet">
        /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
        /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
        /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if an exception has been caught</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>> Handle(GetAllEducationalInstitutionsByBuildingQuery request, CancellationToken cancellationToken = default)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            try
            {
                var queryResult = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                                  .GetAllEducationalInstitutionsWithSameBuildingAsync(request.BuildingID, cancellationToken);

                if (queryResult == default || queryResult.EducationalInstitutions.Count == 0)
                    return new()
                    {
                        Data = null,
                        OperationStatus = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"No Educational Institution that has a building: {request.BuildingID} has been found!"
                    };

                return new()
                {
                    Data = queryResult,
                    StatusCode = HttpStatusCode.OK,
                    OperationStatus = true,
                    Message = string.Empty
                };
            }
            catch (Exception e)
            {
                return HandleException<Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>>(
                            error_message: "Could not find any Educational Institution with BuildingID: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                            response_message: "An error occurred while searching for any Educational Institution with the following BuildingID: {0}!",
                            request.BuildingID,
                            unitOfWork.GetType(),
                            unitOfWork.UsingEducationalInstitutionQueryRepository().GetType(),
                            nameof(IEducationalInstitutionQueryRepository.GetAllEducationalInstitutionsWithSameBuildingAsync),
                            e.Message);
            }
        }
    }
}