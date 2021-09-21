using Identity.API.Application.Services;
using Identity.API.Models;
using Identity.API.Models.AuthorizationViewModels;
using Identity.API.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IIdentityService<User> identityService;

        public AuthorizationController(IIdentityService<User> identityService) => this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        [HttpGet("~/connect/authorize")]
        [HttpPost("~/connect/authorize")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Authorize()
        {
            //get details about the request like tokens, client id, code_challenge, response_type, scopes, claims, redirect_uri etc.
            var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("Could not retrieve the OpenID Connect request!");

            //if a login prompt was requested by the client application, redirect to login page
            if (request.HasPrompt(Prompts.Login))
                await RedirectToLoginPage(request);

            //retrieve the user principal stored in the authentication cookie
            var authenticationResult = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);
            if (authenticationResult == null || !authenticationResult.Succeeded || HasCookieExpired(request, authenticationResult))
            {
                if (request.HasPrompt(Prompts.None))
                    return Forbid(authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                                  properties: new AuthenticationProperties(new Dictionary<string, string>
                                  {
                                      [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.LoginRequired,
                                      [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "User is not logged in!"
                                  }));

                return Challenge(authenticationSchemes: IdentityConstants.ApplicationScheme,
                                 properties: new AuthenticationProperties
                                 {
                                     RedirectUri = Request.PathBase + Request.Path + QueryString.Create(Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                                 });
            }

            var user = await identityService.GetUserProfileAsync(authenticationResult.Principal);

            var application = await identityService.GetClientApplicationAsync(request.ClientId);

            var requestScopes = request.GetScopes();
            var authorizations = await identityService.GetAuthorizationsAsync(subjectId: user.Id,
                                                                              clientId: application.Id,
                                                                              scopes: requestScopes);

            switch (await identityService.GetConsentTypeOfApplicationAsync(application))
            {
                // If the consent is external (e.g when authorizations are granted by a sysadmin),
                // immediately return an error if no authorization can be found in the database.
                case ConsentTypes.External when !authorizations.Any():
                    return Forbid(authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                                  properties: new AuthenticationProperties(new Dictionary<string, string>
                                  {
                                      [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                                      [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Logged in User is not allowed to access this client Application!"
                                  }));

                // If the consent is implicit or if an authorization was found,
                // return an authorization response without displaying the consent form.
                case ConsentTypes.Implicit:
                case ConsentTypes.External when authorizations.Any():
                case ConsentTypes.Explicit when authorizations.Any() && !request.HasPrompt(Prompts.Consent):
                    var principal = await identityService.CreateUserPrincipalAsync(user, application, requestScopes, authorizations);

                    return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                case ConsentTypes.Explicit when request.HasPrompt(Prompts.None):
                case ConsentTypes.Systematic when request.HasPrompt(Prompts.None):
                    return Forbid(authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                                  properties: new AuthenticationProperties(new Dictionary<string, string>
                                  {
                                      [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                                      [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                            "Interactive user consent is required."
                                  }));

                //return a consent form with the resources the application wants to access on behalf of the user
                default:
                    return View(new AuthorizeViewModel
                    {
                        ApplicationName = application.DisplayName,
                        Scope = request.Scope
                    });
            }
        }

        private async Task<IActionResult> RedirectToLoginPage(OpenIddictRequest request)
        {
            //removes the login prompt to avoid endless login -> authorization redirects
            var prompt = string.Join(" ", request.GetPrompts().Remove(Prompts.Login));

            var parameters = Request.HasFormContentType ? Request.Form.Where(parameter => parameter.Key != Parameters.Prompt).ToList() :
                                                          Request.Query.Where(parameter => parameter.Key != Parameters.Prompt).ToList();
            parameters.Add(KeyValuePair.Create(Parameters.Prompt, new StringValues(prompt)));

            return Challenge(authenticationSchemes: IdentityConstants.ApplicationScheme,
                             properties: new AuthenticationProperties
                             {
                                 RedirectUri = Request.PathBase + Request.Path + QueryString.Create(parameters)
                             });
        }

        [Authorize, FormValueRequired("submit.Accept")]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept()
        {
            //get details about the request like tokens, client id, code_challenge, response_type, scopes, claims, redirect_uri etc.
            var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");
            var requestScopes = request.GetScopes();

            var user = await identityService.GetUserProfileAsync(User);

            var application = await identityService.GetClientApplicationAsync(request.ClientId);

            var authorizations = await identityService.GetAuthorizationsAsync(subjectId: user.Id,
                                                                              clientId: application.Id,
                                                                              scopes: requestScopes);

            // Note: the same check is already made in the other action but is repeated
            // here to ensure a malicious user can't abuse this POST-only endpoint and
            // force it to return a valid response without the external authorization.
            if (!authorizations.Any() && await identityService.HasApplicationTheConsentAsync(application, ConsentTypes.External))
                return Forbid(authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                              properties: new AuthenticationProperties(new Dictionary<string, string>
                              {
                                  [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                                  [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                        "The logged in user is not allowed to access this client application."
                              }));

            var principal = await identityService.CreateUserPrincipalAsync(user, application, requestScopes, authorizations);

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Notify OpenIddict that the authorization grant has been denied by the resource owner
        /// to redirect the user agent to the client application using the appropriate response_mode.
        /// </summary>
        [Authorize, FormValueRequired("submit.Deny")]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public IActionResult Deny() => Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        /// <summary>
        /// Exchanges an authorization code for access and id tokens
        /// </summary>
        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            //get details about the request like tokens, client id, code_challenge, response_type, scopes, claims, redirect_uri etc.
            var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType()) throw new InvalidOperationException("The specified grant type is not supported.");

            // Retrieve the claims principal stored in the authorization code/device code/refresh token.
            var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

            // Retrieve the user profile corresponding to the authorization code/refresh token
            // and automatically invalidate the authorization code/refresh token when the user password/roles change
            var user = await identityService.GetValidatedUserAsync(principal);

            if (user == null)
            {
                return Forbid(authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                              properties: new AuthenticationProperties(new Dictionary<string, string>
                              {
                                  [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                                  [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
                              }));
            }

            // Ensure the user is still allowed to sign in.
            if (!await identityService.CanUserStillSignInAsync(user))
            {
                return Forbid(authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                              properties: new AuthenticationProperties(new Dictionary<string, string>
                              {
                                  [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                                  [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is no longer allowed to sign in."
                              }));
            }

            identityService.SetTheDestinationOfEachClaim(principal);

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpGet("~/account/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAccount() => View();

        private bool HasCookieExpired(OpenIddictRequest request, AuthenticateResult result)
        {
            return request.MaxAge != null && result.Properties?.IssuedUtc != null &&
                DateTimeOffset.UtcNow - result.Properties.IssuedUtc > TimeSpan.FromSeconds(request.MaxAge.Value);
        }
    }
}