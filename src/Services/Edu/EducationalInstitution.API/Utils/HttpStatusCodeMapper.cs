using EducationalInstitutionAPI.Proto;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace EducationalInstitutionAPI.Utils
{
    public static class HttpStatusCodeMapper
    {
        private static ConcurrentDictionary<HttpStatusCode, ProtoHttpStatusCode> protoToNetHttpStatusCodeMap;

        static HttpStatusCodeMapper()
        {
            protoToNetHttpStatusCodeMap = new();
        }

        /// <summary>
        /// Extension method that maps a <see cref="HttpStatusCode"/> to a equivalent <see cref="ProtoHttpStatusCode"/> if it can otherwise it maps to <see cref="ProtoHttpStatusCode.Ok"/>
        /// </summary>
        public static void MapToEquivalentProtoHttpStatusCodeOrOK(this HttpStatusCode code, out ProtoHttpStatusCode protoHttpStatusCode)
        {
            if (!protoToNetHttpStatusCodeMap.ContainsKey(code))
            {
                if (Enum.TryParse(code.ToString(), out protoHttpStatusCode))
                    protoToNetHttpStatusCodeMap.TryAdd(code, protoHttpStatusCode);
                else
                    protoHttpStatusCode = ProtoHttpStatusCode.Ok;
            }
            else
                protoToNetHttpStatusCodeMap.TryGetValue(code, out protoHttpStatusCode);
        }
    }
}