using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Threading.Tasks;

namespace Aggregator.Utils
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var call = continuation(request, context);
            return new AsyncUnaryCall<TResponse>(HandleResponseAsync(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
        }

        private static async Task<TResponse> HandleResponseAsync<TResponse>(Task<TResponse> responseFromCall)
        {
            try
            {
                return await responseFromCall;
            }
            catch (RpcException e)
            {
                // _logger.LogError("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                return default;
            }
        }
    }
}