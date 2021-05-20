using EducationalInstitutionAPI.Proto;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace EducationalInstitutionAPI.Utils.Mappers
{
    public static class HttpStatusCodeMapper
    {
        private readonly static ConcurrentDictionary<HttpStatusCode, ProtoHttpStatusCode> protoToNetHttpStatusCodeMap;

        static HttpStatusCodeMapper() => protoToNetHttpStatusCodeMap = new();

        /// <summary>
        /// Extension method that maps a <see cref="HttpStatusCode"/> to an equivalent <see cref="ProtoHttpStatusCode"/> if it can otherwise it maps to <see cref="ProtoHttpStatusCode.Ok"/>
        /// </summary>
        public static ProtoHttpStatusCode MapToEquivalentProtoHttpStatusCodeOrOK(this HttpStatusCode code)
        {
            ProtoHttpStatusCode protoHttpStatusCode;
            if (!protoToNetHttpStatusCodeMap.ContainsKey(code))
            {
                if (Enum.TryParse(code.ToString(), out protoHttpStatusCode))
                    protoToNetHttpStatusCodeMap.TryAdd(code, protoHttpStatusCode);
                else
                    protoHttpStatusCode = ProtoHttpStatusCode.Ok;
            }
            else
                protoToNetHttpStatusCodeMap.TryGetValue(code, out protoHttpStatusCode);

            return protoHttpStatusCode;
        }
    }
}