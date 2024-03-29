﻿using Grpc.Core;

namespace Aggregator.Models;

public record GrpcCallResponse<TResponse>
{
    public TResponse Body { get; init; }
    public Status Status { get; init; }
    public Metadata Trailers { get; init; }
}