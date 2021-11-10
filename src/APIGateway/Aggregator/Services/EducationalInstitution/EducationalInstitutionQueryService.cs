using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Models;

namespace Aggregator.Services.EducationalInstitution;

public class EducationalInstitutionQueryService : GrpcServiceBase<EducationalInstitutionQueryService>,
                                                  IEducationalInstitutionQueryService
{
    private readonly Query.QueryClient client;

    public EducationalInstitutionQueryService(ILogger<EducationalInstitutionQueryService> logger, Query.QueryClient client) : base(logger)
        => this.client = client ?? throw new ArgumentNullException(nameof(client));

    public async Task<GrpcCallResponse<EducationalInstitutionGetResponse>> GetEducationalInstitutionByIDAsync(EducationalInstitutionGetByIdRequest request)
    {
        logger.LogDebug($"{nameof(EducationalInstitutionQueryService)}: {nameof(Query.QueryClient)} calls server with request: {request}");

        var request_call = client.GetEducationalInstitutionByIDAsync(request);

        return await MakeUnaryCallAndGetResponseAsync(request_call);
    }

    public async Task<GrpcCallResponse<EducationalInstitutionGetByNameResponse>> GetAllEducationalInstitutionsByNameAsync(EducationalInstitutionGetByNameRequest request)
    {
        logger.LogDebug($"{nameof(EducationalInstitutionQueryService)}: {nameof(Query.QueryClient)} calls server with request: {request}");

        var request_call = client.GetAllEducationalInstitutionsByNameAsync(request);

        return await MakeUnaryCallAndGetResponseAsync(request_call);
    }

    public async Task<GrpcCallResponse<EducationalInstitutionsGetByLocationResponse>> GetAllEducationalInstitutionsByLocationAsync(EducationalInstitutionsGetByLocationRequest request)
    {
        logger.LogDebug($"{nameof(EducationalInstitutionQueryService)}: {nameof(Query.QueryClient)} calls server with request: {request}");

        var request_call = client.GetAllEducationalInstitutionsByLocationAsync(request);

        return await MakeUnaryCallAndGetResponseAsync(request_call);
    }

    public async Task<GrpcCallResponse<EducationalInstitutionsGetByBuildingResponse>> GetAllEducationalInstitutionsByBuildingAsync(EducationalInstitutionsGetByBuildingRequest request)
    {
        logger.LogDebug($"{nameof(EducationalInstitutionQueryService)}: {nameof(Query.QueryClient)} calls server with request: {request}");

        var request_call = client.GetAllEducationalInstitutionsByBuildingAsync(request);

        return await MakeUnaryCallAndGetResponseAsync(request_call);
    }

    public async Task<GrpcCallResponse<AdminsGetByEducationalInstitutionIdResponse>> GetAllAdminsByEducationalInstitutionIDAsync(AdminsGetByEducationalInstitutionIdRequest request)
    {
        logger.LogDebug($"{nameof(EducationalInstitutionQueryService)}: {nameof(Query.QueryClient)} calls server with request: {request}");

        var request_call = client.GetAllAdminsByEducationalInstitutionIDAsync(request);

        return await MakeUnaryCallAndGetResponseAsync(request_call);
    }
}