using Grpc.Core;
using System.Net;

namespace EducationalInstitutionAPI.Utils.Mappers;

public static class GrpcContextStatusCodeMapper
{
    public static StatusCode ToGrpcContextStatusCode(this HttpStatusCode code)
    {
        switch (code)
        {
            case HttpStatusCode.NotFound:
                return StatusCode.NotFound;

            case HttpStatusCode.InternalServerError:
                return StatusCode.Aborted;

            default:
                return StatusCode.OK;
        }
    }
}