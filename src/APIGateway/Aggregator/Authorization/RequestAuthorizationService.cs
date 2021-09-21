using Aggregator.Authorization.Policies;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Aggregator.Authorization
{
    public class RequestAuthorizationService : IRequestAuthorizationService
    {
        public bool IsRequestValid(ClaimsPrincipal identity, ResourcePolicies policies, out ActionResult response)
        {
            if (identity is null)
            {
                response = new BadRequestObjectResult("Could not retrieve information about the requestor in order to authorize this action!");
                return false;
            }

            if (!AreTheRequiredClaimsPresent(requestorClaims: identity.Claims, policies.constraints))
            {
                response = new ForbidResult(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
                return false;
            }

            response = new OkResult();
            return true;
        }

        private static bool AreTheRequiredClaimsPresent(IEnumerable<Claim> requestorClaims, ResourceConstraints[] constrains)
        {
            foreach (var constraint in constrains)
            {
                var matchedClaims = requestorClaims.Count(claim => constraint.ContainsKeyValuePair(claim.Type, claim.Value));
                if (matchedClaims < constraint.MinimumClaimsNeeded)
                    return false;
            }

            return true;
        }
    }
}