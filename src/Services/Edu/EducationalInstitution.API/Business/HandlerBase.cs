using EducationalInstitutionAPI.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace EducationalInstitutionAPI.Business
{
    /// <summary>
    /// Defines methods that handle exceptions that occur in <typeparamref name="THandler"/>'s methods
    /// </summary>
    public abstract class HandlerBase<THandler> where THandler : class
    {
        /// <summary>
        /// Outputs to a file information about the state of the machine when an error/exception occurs during an operation
        /// </summary>
        private readonly ILogger<THandler> logger;

        protected HandlerBase(ILogger<THandler> logger) => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <summary>
        /// Logs the error and creates a <typeparamref name="TResponse"/> object
        /// </summary>
        /// <typeparam name="TResponse">The return type of the method where the exception was caught</typeparam>
        /// <param name="error_message">The logged message</param>
        /// <param name="response_message">Assigned to the Message field of <typeparamref name="TResponse"/></param>
        /// <param name="error_message_substitutes">Arguments used in the composite format of <paramref name="error_message"/></param>
        /// <returns>
        /// <typeparamref name="TResponse"/> with StatusCode: <see cref="HttpStatusCode.InternalServerError">500</see> and Message: <paramref name="response_message"/>
        /// </returns>
        protected TResponse HandleException<TResponse>(string error_message, string response_message, params object[] error_message_substitutes) where TResponse : Response, new()
        {
            logger.LogError(error_message, error_message_substitutes);
            return new() { StatusCode = HttpStatusCode.InternalServerError, Message = string.Format(response_message, error_message_substitutes) };
        }

        /// <summary>
        /// Logs the error
        /// </summary>
        /// <param name="error_message">The logged message</param>
        /// <param name="error_message_substitutes">Arguments used in the composite format of <paramref name="error_message"/></param>
        /// <returns>False</returns>
        protected bool HandleException(string error_message, params object[] error_message_substitutes)
        {
            logger.LogError(error_message, error_message_substitutes);
            return false;
        }
    }
}