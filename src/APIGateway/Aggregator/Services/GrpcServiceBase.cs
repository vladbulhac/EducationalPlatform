using Aggregator.Models;
using Grpc.Core;

namespace Aggregator.Services;

public abstract class GrpcServiceBase<TService> where TService : class
{
    protected readonly ILogger<TService> logger;

    protected GrpcServiceBase(ILogger<TService> logger)
                => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

    protected async Task<GrpcCallResponse<T>> MakeUnaryCallAndGetResponseAsync<T>(AsyncUnaryCall<T> call)
    {
        try
        {
            var response = await call.ResponseAsync;
            var trailers = call.GetTrailers();

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