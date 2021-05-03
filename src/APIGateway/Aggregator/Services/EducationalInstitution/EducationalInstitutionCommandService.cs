using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public class EducationalInstitutionCommandService : IEducationalInstitutionCommandService
    {
        private readonly ILogger<EducationalInstitutionCommandService> logger;
        private readonly Command.CommandClient client;

        public EducationalInstitutionCommandService(ILogger<EducationalInstitutionCommandService> logger, Command.CommandClient commandClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            client = commandClient ?? throw new ArgumentNullException(nameof(commandClient));
        }

        public async Task<GrpcCallResponse<EducationalInstitutionCreateResponse>> CreateEducationalInstitutionAsync(EducationalInstitutionCreateRequest request)
        {
            //logger.LogDebug("grpc client created, request = {@educationalInstitutionData}", educationalInstitutionData);

            var requestCall = client.CreateEducationalInstitutionAsync(request);

            var response = await requestCall.ResponseAsync;
            var trailers = requestCall.GetTrailers();

            //logger.LogDebug(" grpc response: {@response}", response);

            return new() { Body = response, Trailers = trailers };
        }
    }
}