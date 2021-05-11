using EducationalInstitutionAPI.Utils;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EducationalInstitutionAPI.Grpc
{
    public abstract class ServiceBase
    {
        /// <remarks>
        /// Sets <see cref="ServerCallContext.StatusCode"/> to <see cref="StatusCode.Aborted"/> and the trailer to <see cref="HttpStatusCode.InternalServerError"/>
        /// </remarks>
        protected void HandleException<TClass>(ILogger<TClass> logger, ref ServerCallContext context, string error_message, params object[] error_message_substitutes) where TClass : ServiceBase
        {
            logger.LogError(error_message, error_message_substitutes);
            SetStatusAndTrailersOfContext(ref context, StatusCode.Aborted, "An error occurred while processing the request!", ((int)HttpStatusCode.InternalServerError).ToString());
        }

        protected void SetStatusAndTrailersOfContext(ref ServerCallContext context, StatusCode code, string message, string httpStatusCode)
        {
            SetStatusAndTrailersOfContext(ref context, code, message, new (string key, string value)[2] {
                                                                        ("Message",message),
                                                                        ("HttpStatusCode", httpStatusCode)
                                                                    });
        }

        protected void SetStatusAndTrailersOfContext(ref ServerCallContext context, StatusCode code, string message, (string key, string value)[] trailers_data)
        {
            context.Status = new(code, message);
            context.ResponseTrailers.AddMultiple(trailers_data);
        }

        /// <remarks>
        /// Sets <see cref="ServerCallContext.StatusCode"/> to <see cref="StatusCode.InvalidArgument"/> and the trailer to <see cref="HttpStatusCode.BadRequest"/>
        /// </remarks>
        protected void SetStatusAndTrailersOfContextWhenValidationFails(ref ServerCallContext context, string validationErrors)
                    => SetStatusAndTrailersOfContext(ref context,
                                                  StatusCode.InvalidArgument,
                                                  validationErrors,
                                                  ((int)HttpStatusCode.BadRequest).ToString());
    }
}