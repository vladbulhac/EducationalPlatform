using System.Net;

namespace EducationaInstitutionAPI.Utils
{
    public record Response<ResType>
    {
        public ResType ResponseObject { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public bool OperationStatus { get; init; }
        public string Message { get; init; }
    }
}