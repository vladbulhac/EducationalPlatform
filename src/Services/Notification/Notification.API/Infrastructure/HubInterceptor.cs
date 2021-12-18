using DataValidation.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Notification.API.Hubs.DataTransferObjects;
using System.Net;
using System.Reflection;

namespace Notification.API.Infrastructure;

public class HubInterceptor : IHubFilter
{
    private readonly ILogger<HubInterceptor> logger;
    private readonly IValidationHandler validationHandler;

    public HubInterceptor(IValidationHandler validationHandler, ILogger<HubInterceptor> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.validationHandler = validationHandler ?? throw new ArgumentNullException(nameof(validationHandler));
    }

    public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
    {
        logger.LogDebug($"[SignalR.Hub.Interceptor]: Calling hub method '{invocationContext.HubMethodName}'");

        try
        {
            if (invocationContext.HubMethodArguments.Count > 0)
                await ValidateCallArgument(invocationContext);

            return await next(invocationContext);
        }
        catch (HubException ex)
        {
            logger.LogDebug($"[SignalR.Hub.Interceptor]: Exception calling '{invocationContext.HubMethodName}', error details => {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogDebug($"[SignalR.Hub.Interceptor]: Exception calling '{invocationContext.HubMethodName}', error details => {ex.Message}");
            throw new HubException();
        }
    }

    private async Task ValidateCallArgument(HubInvocationContext invocationContext)
    {
        var argument = invocationContext.HubMethodArguments.First();
        var argumentRuntimeType = argument.GetType();
        var dto = Convert.ChangeType(argument, argumentRuntimeType);

        var validationMethod = GetValidationMethod(argumentRuntimeType);

        //after invoking the method, the item on index = 1 will be the value of the method's out argument, validationErrors
        var methodParameters = new object[2] { dto, null };

        if (!(bool)validationMethod.Invoke(validationHandler, methodParameters))
        {
            string validationErrors = (string)methodParameters[1];

            await invocationContext.Hub.Clients
                                       .Caller.SendAsync("CallFailed", new HubCallFailDetails(HttpStatusCode.BadRequest, validationErrors));

            //throwing an exception prevents the hub method invocation with next()
            throw new HubException(validationErrors);
        }
    }

    private MethodInfo GetValidationMethod(Type genericTypeParameter)
    {
        var validationMethod = typeof(IValidationHandler).GetMethods()
                                                         .First(m => m.Name == nameof(IValidationHandler.IsDataTransferObjectValid));
        return validationMethod.MakeGenericMethod(genericTypeParameter);
    }
}