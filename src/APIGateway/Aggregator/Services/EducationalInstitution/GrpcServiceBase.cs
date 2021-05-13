using Aggregator.DTOs;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public abstract class GrpcServiceBase<TService> where TService : class
    {
        protected readonly ILogger<TService> logger;

        protected GrpcServiceBase(ILogger<TService> logger)
                    => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        protected async Task<GrpcCallResponse<T>> MakeUnaryCallAndGetResponseAsync<T>(AsyncUnaryCall<T> request_call)
        {
            try
            {
                var response = await request_call.ResponseAsync;
                var trailers = request_call.GetTrailers();

                logger.LogDebug("gRPC call ended with response: {@response}", response);

                return new() { Body = response, Trailers = trailers };
            }
            catch (Exception e)
            {
                logger.LogError($"Could not finish the gRPC call successfully, error details => {e.Message}");
                return new() { Body = default, Trailers = new() };
            }
        }
    }
}