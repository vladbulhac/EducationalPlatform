using Grpc.Core;
using System.Net;

namespace EducationalInstitutionAPI.Utils.Mappers
{
    public static class RPCCallContextStatusCodeMapper
    {
        public static StatusCode ToRPCCallContextStatusCode(this HttpStatusCode code)
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
}