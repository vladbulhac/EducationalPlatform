using EducationalInstitution.Application.BaseHandlers;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EducationalInstitution.Application.Queries.Handlers;

public class GetEducationalInstitutionByIDQueryHandler : HandlerBase<GetEducationalInstitutionByIDQueryHandler>,
                                                         IRequestHandler<GetEducationalInstitutionByIDQuery, Response<GetEducationalInstitutionByIDQueryResult>>
{
    private readonly IUnitOfWorkForQueries unitOfWork;

    /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
    public GetEducationalInstitutionByIDQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetEducationalInstitutionByIDQueryHandler> logger) : base(logger)
        => this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <summary>
    /// Tries to get an <see cref="EducationalInstitution"/> by EducationalInstitutionID
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
    public async Task<Response<GetEducationalInstitutionByIDQueryResult>> Handle(GetEducationalInstitutionByIDQuery request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        try
        {
            var queryResult = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                              .GetByIDAsync(request.EducationalInstitutionID, cancellationToken);

            if (queryResult == default)
                return new()
                {
                    Data = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Educational Institution with the following ID: {request.EducationalInstitutionID} has not been found!"
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
            return HandleException<Response<GetEducationalInstitutionByIDQueryResult>>(
                        error_message: "Could not find an Educational Institution with ID: {0}, using {1}'s method: {2}, error details => {3}",
                        response_message: "An error occurred while searching for the Educational Institution with the following ID: {0}!",
                        request.EducationalInstitutionID,
                        unitOfWork.GetType(),
                        nameof(IEducationalInstitutionQueryRepository.GetByIDAsync),
                        e.Message);
        }
    }
}