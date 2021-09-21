using Identity.API.Configuration.Client_Scopes;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API.Configuration.Clients
{
    public class Clients : IClients<OpenIddictApplicationDescriptor>
    {
        private OpenIddictApplicationDescriptor spaClient;

        public Clients() => ConfigureSinglePageApplicationClient();

        public IEnumerable<OpenIddictApplicationDescriptor> GetClients()
        {
            yield return spaClient;
        }

        private void ConfigureSinglePageApplicationClient()
        {
            spaClient = new OpenIddictApplicationDescriptor
            {
                ClientId = "spa-client",
                //ClientSecret = "spa-53Cr37",
                ConsentType = ConsentTypes.Implicit,
                DisplayName = "Single Page Application Client",
                Type = ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:41110/authentication/logout-callback")
                },
                RedirectUris =
                {
                    new Uri("https://oauth.pstmn.io/v1/callback"),
                    new Uri("https://localhost:41110/authentication/login-callback")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + DefinedScopes.EducationalInstitutionScopes.All,
                        Permissions.Prefixes.Scope + DefinedScopes.NotificationScopes.All
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            };
        }
    }
}