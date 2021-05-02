using Aggregator.Common.Proto;
using System.Net;

namespace Aggregator.Utils
{
    public static class HttpStatusCodeConverter
    {
        public static HttpStatusCode DecodeStatusCode(this ProtoHttpStatusCode protoStatusCode)
        {
            switch (protoStatusCode)
            {
                case ProtoHttpStatusCode.Created:
                    return HttpStatusCode.Created;

                case ProtoHttpStatusCode.MultiStatus:
                    return HttpStatusCode.MultiStatus;

                case ProtoHttpStatusCode.BadRequest:
                    return HttpStatusCode.BadRequest;

                case ProtoHttpStatusCode.InternalServerError:
                    return HttpStatusCode.InternalServerError;

                default:
                    return HttpStatusCode.ServiceUnavailable;
            }
        }
    }
}