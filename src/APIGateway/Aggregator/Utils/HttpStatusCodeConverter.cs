using Aggregator.EducationaInstitutionAPI.Proto;

namespace Aggregator.Utils
{
    public static class HttpStatusCodeConverter
    {
        public static System.Net.HttpStatusCode DecodeStatusCode(this HttpStatusCode protoStatusCode)
        {
            switch (protoStatusCode)
            {
                case HttpStatusCode.Created:
                    return System.Net.HttpStatusCode.Created;

                case HttpStatusCode.MultiStatus:
                    return System.Net.HttpStatusCode.MultiStatus;

                case HttpStatusCode.BadRequest:
                    return System.Net.HttpStatusCode.BadRequest;

                case HttpStatusCode.InternalServerError:
                    return System.Net.HttpStatusCode.InternalServerError;

                default:
                    return System.Net.HttpStatusCode.ServiceUnavailable;
            }
        }
    }
}