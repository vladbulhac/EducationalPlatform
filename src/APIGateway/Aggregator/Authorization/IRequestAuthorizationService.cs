using Aggregator.Authorization.Policies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aggregator.Authorization
{
    /// <summary>
    /// Defines methods that are used to verify if the requestor is authorized (has the necessary claims) to do a certain action
    /// </summary>
    public interface IRequestAuthorizationService
    {
        /// <returns>
        /// <list type="bullet">
        /// <item>False with <see cref="BadRequestObjectResult"/> if <paramref name="identity"/> is null</item>
        /// <item>False with <see cref="ForbidResult"/> if not all <paramref name="requiredClaims"/> are matched in <see cref="ClaimsPrincipal.Claims">identity</see></item>
        /// <item>True with <see cref="OkResult"/> if requestor is authorized to do the action</item>
        /// </list>
        /// </returns>
        public bool IsRequestValid(ClaimsPrincipal identity, ResourcePolicies policies, out ActionResult response);
    }
}