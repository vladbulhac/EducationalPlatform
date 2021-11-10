using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Net.Http.Headers;

namespace Aggregator.Infrastructure;

public class GrpcInterceptor : Interceptor
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<GrpcInterceptor> logger;

    public GrpcInterceptor(IHttpContextAccessor httpContextAccessor, ILogger<GrpcInterceptor> logger)
    {
        this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var headers = CreateHeaders();

        var optionsWithHeaders = context.Options.WithHeaders(headers);

        var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method,
                                                                           context.Host,
                                                                           optionsWithHeaders);

        var call = continuation(request, newContext);
        return new AsyncUnaryCall<TResponse>(HandleResponseAsync(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
    }

    private Metadata CreateHeaders()
    {
        var headers = new Metadata();

        AddAccessTokenToHeaders(headers);
        TryToAddResourceIdToHeaders(headers);

        return headers;
    }

    private void AddAccessTokenToHeaders(Metadata headers)
    {
        var accessToken = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization]
                                               .ToString();

        headers.Add(new Metadata.Entry(HeaderNames.Authorization, accessToken));
    }

    private void TryToAddResourceIdToHeaders(Metadata headers)
    {
        httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("resourceId", out object resourceId);

        if (resourceId is not null)
            headers.Add(new Metadata.Entry("resource_id", resourceId.ToString()));
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