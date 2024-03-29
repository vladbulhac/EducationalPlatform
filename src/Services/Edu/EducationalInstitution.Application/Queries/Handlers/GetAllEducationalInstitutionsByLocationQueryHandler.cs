﻿using EducationalInstitution.Application.BaseHandlers;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EducationalInstitution.Application.Queries.Handlers;

public class GetAllEducationalInstitutionsByLocationQueryHandler : HandlerBase<GetAllEducationalInstitutionsByLocationQueryHandler>,
                                                                   IRequestHandler<GetAllEducationalInstitutionsByLocationQuery,
                                                                                   Response<GetAllEducationalInstitutionsByLocationQueryResult>>
{
    private readonly IUnitOfWorkForQueries unitOfWork;

    /// <inheritdoc cref="HandlerBase{THandler}.HandlerBase"/>
    public GetAllEducationalInstitutionsByLocationQueryHandler(IUnitOfWorkForQueries unitOfWork, ILogger<GetAllEducationalInstitutionsByLocationQueryHandler> logger) : base(logger)
        => this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <summary>
    /// Tries to get an <see cref="EducationalInstitution"/> by LocationID.
    /// </summary>
    /// <returns>
    /// An <see cref="Response{ResponseType}">object</see> with HttpStatusCode:
    /// <list type="bullet">
    /// <item><see cref="HttpStatusCode.OK">Ok</see> if operation is successful</item>
    /// <item><see cref="HttpStatusCode.NotFound">NotFound</see> if no <see cref="EducationalInstitution"/> has been found for the provided id</item>
    /// <item><see cref="HttpStatusCode.InternalServerError">InternalServerError</see> if an exception has been caught</item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException"/>
    public async Task<Response<GetAllEducationalInstitutionsByLocationQueryResult>> Handle(GetAllEducationalInstitutionsByLocationQuery request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        try
        {
            var queryResult = await unitOfWork.UsingEducationalInstitutionQueryRepository()
                                              .GetAllByLocationAsync(request.LocationID, cancellationToken);

            if (queryResult == default || queryResult.EducationalInstitutions.Count == 0)
                return new()
                {
                    Data = null,
                    OperationStatus = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"No Educational Institution with the following LocationID: {request.LocationID} has been found!"
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
            return HandleException<Response<GetAllEducationalInstitutionsByLocationQueryResult>>(
                        error_message: "Could not find an Educational Institution with LocationID: {0}, using {1} with {2}'s method: {3}, error details => {4}",
                        response_message: "An error occurred while searching for the Educational Institution with the following LocationID: {0}!",
                        request.LocationID,
                        unitOfWork.GetType(),
                        unitOfWork.UsingEducationalInstitutionQueryRepository().GetType(),
                        nameof(IEducationalInstitutionQueryRepository.GetAllByLocationAsync),
                        e.Message);
        }
    }
}