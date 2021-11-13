using EducationalInstitutionAPI.Proto;
using System.Collections.Concurrent;
using System.Net;

namespace EducationalInstitutionAPI.Utils.Mappers;

public static class HttpStatusCodeMapper
{
    private static readonly ConcurrentDictionary<HttpStatusCode, ProtoHttpStatusCode> httpStatusCodeToProtoMap;

    static HttpStatusCodeMapper() => httpStatusCodeToProtoMap = new();

    /// <summary>
    /// Extension method that maps a <see cref="HttpStatusCode"/> to an equivalent <see cref="ProtoHttpStatusCode"/> if it can otherwise it maps to <see cref="ProtoHttpStatusCode.Ok"/>
    /// </summary>
    public static ProtoHttpStatusCode ToProtoHttpStatusCode(this HttpStatusCode code)
    {
        ProtoHttpStatusCode protoHttpStatusCode;
        if (!httpStatusCodeToProtoMap.ContainsKey(code))
        {
            if (Enum.TryParse(code.ToString(), out protoHttpStatusCode))
                httpStatusCodeToProtoMap.TryAdd(code, protoHttpStatusCode);
            else
                protoHttpStatusCode = ProtoHttpStatusCode.Ok;
        }
        else
            httpStatusCodeToProtoMap.TryGetValue(code, out protoHttpStatusCode);

        return protoHttpStatusCode;
    }
}