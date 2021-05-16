using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aggregator.Utils
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly ILogger<GrpcExceptionInterceptor> logger;

        public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger) => this.logger = logger;

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var call = continuation(request, context);
            return new AsyncUnaryCall<TResponse>(HandleResponseAsync(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
        }

        private async Task<TResponse> HandleResponseAsync<TResponse>(Task<TResponse> responseFromCall)
        {
            try
            {
                return await responseFromCall;
            }
            catch (RpcException e)
            {
                logger.LogError("Error encountered during a gRPC call: {Status} - {Message}", e.Status, e.Message);
                return default;
            }
        }
    }
}