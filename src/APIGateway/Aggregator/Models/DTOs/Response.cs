using System.Net;

namespace EducationaInstitutionAPI.Utils
{
    /// <summary>
    /// Contains the response to a request and the status of the request operation
    /// </summary>
    public record Response<ResponseType>
    {
        /// <summary>
        /// Contains the result of the request operation
        /// </summary>
        /// <value>If <see cref="OperationStatus"/> is True a <typeparamref name="ResponseType"/> object that contains the requested data, NULL otherwise</value>
        public ResponseType ResponseObject { get; init; }
        public HttpStatusCode StatusCode { get; init; }

        /// <summary>
        /// Describes if the request operation was successful
        /// </summary>
        public bool OperationStatus { get; init; }

        /// <summary>
        /// Additional information for the client regarding the request
        /// </summary>
        /// <value>An error/exception/custom description if necessary, Empty otherwise</value>
        public string Message { get; init; }
    }
}