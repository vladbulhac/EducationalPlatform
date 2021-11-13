using EducationalInstitutionAPI.Utils;
using EducationalInstitutionAPI.Utils.Mappers;
using Grpc.Core;
using System.Net;

namespace EducationalInstitutionAPI.Presentation.Grpc;

public abstract class ServiceBase
{
    /// <remarks>
    /// <i>Sets <see cref="ServerCallContext.StatusCode"/> to <see cref="StatusCode.Aborted"/> and the trailer to <see cref="HttpStatusCode.InternalServerError"/></i>
    /// </remarks>
    protected void HandleException<TClass>(ILogger<TClass> logger, ref ServerCallContext context, string error_message, params object[] error_message_substitutes) where TClass : ServiceBase
    {
        logger.LogError(error_message, error_message_substitutes);
        SetStatusAndTrailersOfContext(ref context,
                                     StatusCode.Aborted,
                                     "An error occurred while processing the request!",
                                     HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Adds <paramref name="message"/> and <paramref name="httpStatusCode"/> trailers to the context
    /// </summary>
    protected void SetStatusAndTrailersOfContext(ref ServerCallContext context, StatusCode code, string message, HttpStatusCode httpStatusCode)
          => SetStatusAndTrailersOfContext(ref context, code, message, new (string key, string value)[2] {
                                                                                ("Message",message),
                                                                                ("HttpStatusCode", ((int)httpStatusCode).ToString())
                                                                             });

    /// <remarks>
    /// <i>Converts <paramref name="httpStatusCode"/> to equivalent <see cref="StatusCode"/></i>
    /// </remarks>
    protected void SetStatusAndTrailersOfContext(ref ServerCallContext context, HttpStatusCode httpStatusCode, string message)
            => SetStatusAndTrailersOfContext(ref context, httpStatusCode.ToGrpcContextStatusCode(), message, httpStatusCode);

    protected void SetStatusAndTrailersOfContext(ref ServerCallContext context, StatusCode code, string message, (string key, string value)[] trailers_data)
    {
        context.Status = new(code, message);
        context.ResponseTrailers.AddMultiple(trailers_data);
    }

    /// <remarks>
    /// <i>Sets <see cref="ServerCallContext.StatusCode"/> to <see cref="StatusCode.InvalidArgument"/> and the trailer to <see cref="HttpStatusCode.BadRequest"/></i>
    /// </remarks>
    protected void SetStatusAndTrailersOfContextWhenValidationFails(ref ServerCallContext context, string validationErrors)
                => SetStatusAndTrailersOfContext(ref context,
                                                 StatusCode.InvalidArgument,
                                                 validationErrors,
                                                 HttpStatusCode.BadRequest);
}