using Identity.API.Configuration.Client_Scopes;
using Identity.API.Configuration.User_Permissions;
using OpenIddict.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API.Configuration
{
    public static class ClaimDestinationManager
    {
        public static IEnumerable<string> DecideDestination(Claim claim, ClaimsPrincipal principal)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Profile))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Roles))
                        yield return Destinations.IdentityToken;

                    yield break;

                case DefinedUserPermissions.EducationalInstitutionPermissions.ChangeAdministrators when claim.Value is "granted":
                    yield return Destinations.IdentityToken;

                    if (principal.HasScope(DefinedScopes.EducationalInstitutionScopes.ChangeAdministrators) || principal.HasScope(DefinedScopes.EducationalInstitutionScopes.All))
                        yield return Destinations.AccessToken;

                    yield break;

                case DefinedUserPermissions.EducationalInstitutionPermissions.All when claim.Value is "granted":
                    yield return Destinations.IdentityToken;

                    if (principal.HasScope(DefinedScopes.EducationalInstitutionScopes.All))
                        yield return Destinations.AccessToken;

                    yield break;

                case DefinedUserPermissions.EducationalInstitutionPermissions.Delete when claim.Value is "granted":
                    yield return Destinations.IdentityToken;

                    if (principal.HasScope(DefinedScopes.EducationalInstitutionScopes.Delete) || principal.HasScope(DefinedScopes.EducationalInstitutionScopes.All))
                        yield return Destinations.AccessToken;

                    yield break;

                case DefinedUserPermissions.EducationalInstitutionPermissions.UpdateDetails when claim.Value is "granted":
                    yield return Destinations.IdentityToken;

                    if (principal.HasScope(DefinedScopes.EducationalInstitutionScopes.UpdateDetails) || principal.HasScope(DefinedScopes.EducationalInstitutionScopes.All))
                        yield return Destinations.AccessToken;

                    yield break;

                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }
    }
}