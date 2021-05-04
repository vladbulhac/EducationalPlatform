using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public class EducationalInstitutionCommandService : GrpcServiceBase, IEducationalInstitutionCommandService
    {
        private readonly ILogger<EducationalInstitutionCommandService> logger;
        private readonly Command.CommandClient client;

        public EducationalInstitutionCommandService(ILogger<EducationalInstitutionCommandService> logger, Command.CommandClient commandClient) : base(logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            client = commandClient ?? throw new ArgumentNullException(nameof(commandClient));
        }

        public async Task<GrpcCallResponse<EducationalInstitutionCreateResponse>> CreateEducationalInstitutionAsync(EducationalInstitutionCreateRequest request)
        {
            logger.LogDebug("Grpc client created, calls server with request: {@request}", request);

            var request_call = client.CreateEducationalInstitutionAsync(request);

            return await MakeUnaryCallAndGetResponseAsync(request_call);
        }
    }
}