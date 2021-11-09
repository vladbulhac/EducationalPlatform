using Aggregator.Common.Proto;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace Aggregator.Models.ObjectMappers;

public static class HttpStatusCodeMapper
{
    private static readonly ConcurrentDictionary<ProtoHttpStatusCode, HttpStatusCode> protoToHttpStatusCodeMap;

    static HttpStatusCodeMapper() => protoToHttpStatusCodeMap = new();

    /// <summary>
    /// Extension method that maps a <see cref="ProtoHttpStatusCode"/> to an equivalent <see cref="HttpStatusCode"/> if it can otherwise it maps to <see cref="HttpStatusCode.OK"/>
    /// </summary>
    public static HttpStatusCode ToHttpStatusCode(this ProtoHttpStatusCode code)
    {
        HttpStatusCode httpStatusCode;
        if (!protoToHttpStatusCodeMap.TryGetValue(code, out httpStatusCode))
        {
            if (Enum.TryParse(code.ToString(), out httpStatusCode))
                protoToHttpStatusCodeMap.TryAdd(code, httpStatusCode);
            else
                httpStatusCode = HttpStatusCode.OK;
        }

        return httpStatusCode;
    }
}