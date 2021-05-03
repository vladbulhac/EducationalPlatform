using Grpc.Core;

namespace Aggregator.DTOs
{
    public record GrpcCallResponse<TResponse>
    {
        public TResponse Body { get; init; }
        public Metadata Trailers { get; init; }
    }
}