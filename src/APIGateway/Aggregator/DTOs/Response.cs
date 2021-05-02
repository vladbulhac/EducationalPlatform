using System.Net;

namespace Aggregator.DTOs
{
    /// <summary>
    /// Extends <see cref="Response"/> with a result object
    /// </summary>
    public record Response<TData> : Response
    {
        /// <summary>
        /// Contains the result of the request operation
        /// </summary>
        /// <value>Encapsulates the result data when a request is successful, NULL otherwise</value>
        public TData Data { get; init; }
    }

    /// <summary>
    /// Contains the status of the requested operation
    /// </summary>
    public record Response
    {
        public HttpStatusCode StatusCode { get; init; }

        /// <summary>
        /// Describes if the requested operation was successful
        /// </summary>
        public bool OperationStatus { get; init; }

        /// <summary>
        /// Additional information for the client regarding the request
        /// </summary>
        /// <value>An error/exception/custom description if necessary, Empty otherwise</value>
        public string Message { get; init; }
    }
}