using System.Net;

namespace EducationalInstitutionAPI.DTOs
{
    /// <summary>
    /// Contains the response object to a request and the fields of <see cref="Response"/>
    /// </summary>
    public record Response<TResponse> : Response
    {
        /// <summary>
        /// Contains the result of the request operation
        /// </summary>
        /// <value>If <see cref="OperationStatus"/> is True a <typeparamref name="TResponse"/> object that contains the requested data, NULL otherwise</value>
        public TResponse ResponseObject { get; init; }
    }

    /// <summary>
    /// Contains the status of the request operation
    /// </summary>
    public record Response
    {
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