using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public class EducationalInstitutionCommandService : GrpcServiceBase<EducationalInstitutionCommandService>,
                                                        IEducationalInstitutionCommandService
    {
        private readonly Command.CommandClient client;

        public EducationalInstitutionCommandService(ILogger<EducationalInstitutionCommandService> logger, Command.CommandClient commandClient) : base(logger)
            => client = commandClient ?? throw new ArgumentNullException(nameof(commandClient));

        public async Task<GrpcCallResponse<EducationalInstitutionCreateResponse>> CreateEducationalInstitutionAsync(EducationalInstitutionCreateRequest request)
        {
            logger.LogDebug("Command.CommandClient calls server with request: {@request}", request);

            var request_call = client.CreateEducationalInstitutionAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }

        public async Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionAsync(EducationalInstitutionUpdateRequest request)
        {
            logger.LogDebug("Command.CommandClient calls server with request: {@request}", request);

            var request_call = client.UpdateEducationalInstitutionAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }

        public async Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionParentAsync(EducationalInstitutionParentUpdateRequest request)
        {
            logger.LogDebug("Command.CommandClient calls server with request: {@request}", request);

            var request_call = client.UpdateEducationalInstitutionParentAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }

        public async Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionAdminAsync(EducationalInstitutionAdminUpdateRequest request)
        {
            logger.LogDebug("Command.CommandClient calls server with request: {@request}", request);

            var request_call = client.UpdateEducationalInstitutionAdminAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }

        public async Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionLocationAsync(EducationalInstitutionLocationUpdateRequest request)
        {
            logger.LogDebug("Command.CommandClient calls server with request: {@request}", request);

            var request_call = client.UpdateEducationalInstitutionLocationAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }

        public async Task<GrpcCallResponse<EducationalInstitutionDeleteResponse>> DeleteEducationalInstitutionAsync(EducationalInstitutionDeleteRequest request)
        {
            logger.LogDebug("Command.CommandClient calls server with request: {@request}", request);

            var request_call = client.DeleteEducationalInstitutionAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }
    }
}