using Microsoft.AspNetCore.Identity;
using OpenIddict.EntityFrameworkCore.Models;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API.Application.Services;

/// <summary>
/// Exposes a simplified interface with authorization and authentication operations
/// </summary>
public interface IIdentityService<TUser> where TUser : IdentityUser
{
    public Task<TUser> GetUserProfileAsync(ClaimsPrincipal principal);

    /// <summary>
    /// Ensures that the user's permissions/password didn't change during the authorization process
    /// </summary>
    public Task<TUser> GetValidatedUserAsync(ClaimsPrincipal principal);

    public Task<bool> CanUserStillSignInAsync(TUser user);

    /// <remarks><i>
    /// By default, claims are NOT automatically included in the access and identity tokens.
    /// To allow OpenIddict to serialize them, you must attach them a destination, that specifies
    /// whether they should be included in access tokens, in identity tokens or in both.
    /// </i></remarks>
    public void SetTheDestinationOfEachClaim(ClaimsPrincipal principal);

    /// <exception cref="InvalidOperationException"/>
    public Task<OpenIddictEntityFrameworkCoreApplication> GetClientApplicationAsync(string clientId);

    /// <summary>
    /// Returns a list of <see cref="OpenIddictEntityFrameworkCoreAuthorization"/> associated with the user and the calling client application
    /// </summary>
    /// <param name="subjectId">User's Id</param>
    /// <param name="clientId">Application's Id</param>
    /// <param name="scopes">Request's scopes</param>
    public Task<List<object>> GetAuthorizationsAsync(string subjectId, string clientId, ImmutableArray<string> scopes);

    /// <summary>
    /// Returns a string that matches one of the <see cref="ConsentTypes"/>
    /// </summary>
    public Task<string> GetConsentTypeOfApplicationAsync(OpenIddictEntityFrameworkCoreApplication application);

    public Task<bool> HasApplicationTheConsentAsync(OpenIddictEntityFrameworkCoreApplication application, string consentType);

    /// <summary>
    /// Configures a new <see cref="ClaimsPrincipal"/> with scopes, resources, authorizations and the destinations of each claim
    /// </summary>
    public Task<ClaimsPrincipal> CreateUserPrincipalAsync(TUser user, OpenIddictEntityFrameworkCoreApplication application, ImmutableArray<string> scopes, List<object> authorizations);
}