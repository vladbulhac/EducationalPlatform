using Aggregator.DTOs;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public abstract class GrpcServiceBase
    {
        private readonly ILogger logger;

        protected GrpcServiceBase(ILogger logger)
        {
            this.logger = logger;
        }

        protected async Task<GrpcCallResponse<T>> MakeUnaryCallAndGetResponseAsync<T>(AsyncUnaryCall<T> request_call)
        {
            var response = await request_call.ResponseAsync;
            var trailers = request_call.GetTrailers();

            logger.LogDebug("Grpc call ended with response: {@response}", response);

            return new() { Body = response, Trailers = trailers };
        }
    }
}