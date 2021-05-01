using EducationalInstitutionAPI.Proto;
using System;
using System.Collections.Concurrent;

namespace EducationalInstitutionAPI.Utils
{
    public static class HttpStatusCodeMapper
    {
        private static ConcurrentDictionary<System.Net.HttpStatusCode, HttpStatusCode> protoToNetHttpStatusCodeMap;

        static HttpStatusCodeMapper()
        {
            protoToNetHttpStatusCodeMap = new();
        }

        public static bool CanMapToProto(this System.Net.HttpStatusCode code, out HttpStatusCode protoStatusCode)
        {
            protoStatusCode = HttpStatusCode.Default;

            if (!protoToNetHttpStatusCodeMap.ContainsKey(code))
            {
                if (!Enum.TryParse(code.ToString(), out HttpStatusCode protoCode))
                    return false;

                protoToNetHttpStatusCodeMap.TryAdd(code, protoCode);

                protoStatusCode = protoCode;
                return true;
            }

            return protoToNetHttpStatusCodeMap.TryGetValue(code, out protoStatusCode);
        }
    }
}