using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public class EducationalInstitutionQueryService : GrpcServiceBase<EducationalInstitutionQueryService>, IEducationalInstitutionQueryService
    {
        private readonly Query.QueryClient client;

        public EducationalInstitutionQueryService(ILogger<EducationalInstitutionQueryService> logger, Query.QueryClient client) : base(logger)
            => this.client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<GrpcCallResponse<EducationalInstitutionGetResponse>> GetEducationalInstitutionByIDAsync(EducationalInstitutionGetByIdRequest request)
        {
            logger.LogDebug("gRPC client created, calls server with request: {@request}", request);

            var request_call = client.GetEducationalInstitutionByIDAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }
    }
}