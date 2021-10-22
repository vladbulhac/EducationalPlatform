using EducationalInstitutionAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Authorization.Handlers
{
    public class DeleteEducationalInstitutionAuthorizationHandler : AuthorizationHandler<DeleteEducationalInstitutionRequirements>
    {
        private readonly IHttpContextAccessor contextAccessor;

        public DeleteEducationalInstitutionAuthorizationHandler(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteEducationalInstitutionRequirements requirement)
        {
            if (!RequestContainsTheResourceIdClientWantsToAccess(out string resourceId)) return Task.CompletedTask;

            if (AreTheRequiredClaimsPresent(context.User.Claims, requirement.Policy.constraints, resourceId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool RequestContainsTheResourceIdClientWantsToAccess(out string resourceId)
        {
            contextAccessor.HttpContext.Request.Headers.TryGetValue("resource_id", out StringValues headerValues);
            resourceId = headerValues.FirstOrDefault();

            return !string.IsNullOrEmpty(resourceId);
        }

        //user is authorized if a claim like "permission_name:resourceId" exists
        //and the resourceId of permission matches the resourceId the client tries to access
        private static bool AreTheRequiredClaimsPresent(IEnumerable<Claim> requestorClaims, ResourceAccessConstraints[] constrains, string resourceId)
        {
            foreach (var constraint in constrains)
            {
                var matchedClaims = requestorClaims.Count(claim => constraint.ContainsKey(claim.Type) && claim.Value.Equals(resourceId));
                if (matchedClaims < constraint.MinimumClaimsNeeded)
                    return false;
            }

            return true;
        }
    }
}