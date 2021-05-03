using Aggregator.Common.Proto;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace Aggregator.Utils
{
    public static class HttpStatusCodeMapper
    {
        private static ConcurrentDictionary<ProtoHttpStatusCode, HttpStatusCode> httpStatusCodeToProtoMap;

        static HttpStatusCodeMapper() => httpStatusCodeToProtoMap = new();

        /// <summary>
        /// Extension method that maps a <see cref="ProtoHttpStatusCode"/> to an equivalent <see cref="HttpStatusCode"/> if it can otherwise it maps to <see cref="HttpStatusCode.OK"/>
        /// </summary>
        public static void MapToEquivalentHttpStatusCodeOrOK(this ProtoHttpStatusCode code, out HttpStatusCode httpStatusCode)
        {
            if (!httpStatusCodeToProtoMap.ContainsKey(code))
            {
                if (Enum.TryParse(code.ToString(), out httpStatusCode))
                    httpStatusCodeToProtoMap.TryAdd(code, httpStatusCode);
                else
                    httpStatusCode = HttpStatusCode.OK;
            }
            else
                httpStatusCodeToProtoMap.TryGetValue(code, out httpStatusCode);
        }

        /// <summary>
        /// Extension method that maps a <see cref="ProtoHttpStatusCode"/> to an equivalent <see cref="HttpStatusCode"/> if it can otherwise it maps to <see cref="HttpStatusCode.OK"/>
        /// </summary>
        public static HttpStatusCode MapToEquivalentProtoHttpStatusCodeOrOK(this ProtoHttpStatusCode code)
        {
            code.MapToEquivalentHttpStatusCodeOrOK(out HttpStatusCode httpStatusCode);
            return httpStatusCode;
        }
    }
}