using Identity.API.Configuration;
using Identity.API.Models;
using Identity.API.Utils;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API.Application.Services;

public class IdentityService : IIdentityService<User>
{
    private readonly IOpenIddictApplicationManager applicationManager;
    private readonly IOpenIddictAuthorizationManager authorizationManager;
    private readonly IOpenIddictScopeManager scopeManager;
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;

    public IdentityService(IOpenIddictApplicationManager appManager, IOpenIddictAuthorizationManager authManager, IOpenIddictScopeManager scopeManager, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        applicationManager = appManager ?? throw new ArgumentNullException(nameof(appManager));
        authorizationManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
        this.scopeManager = scopeManager ?? throw new ArgumentNullException(nameof(scopeManager));
        this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<User> GetValidatedUserAsync(ClaimsPrincipal principal) => await signInManager.ValidateSecurityStampAsync(principal);

    public async Task<bool> CanUserStillSignInAsync(User user) => await signInManager.CanSignInAsync(user);

    /// <exception cref="InvalidOperationException"/>
    public async Task<User> GetUserProfileAsync(ClaimsPrincipal principal) => await userManager.GetUserAsync(principal) ?? throw new InvalidOperationException("User information cannot be retrieved!");

    /// <exception cref="InvalidOperationException"/>
    public async Task<OpenIddictEntityFrameworkCoreApplication> GetClientApplicationAsync(string clientId)
                 => await applicationManager.FindByClientIdAsync(clientId) as OpenIddictEntityFrameworkCoreApplication ?? throw new InvalidOperationException("Application information cannot be retrieved!");

    public async Task<List<object>> GetAuthorizationsAsync(string subjectId, string clientId, ImmutableArray<string> scopes)
                 => await authorizationManager.FindAsync(subject: subjectId,
                                                         client: clientId,
                                                         status: Statuses.Valid,
                                                         type: AuthorizationTypes.Permanent,
                                                         scopes: scopes).ToListAsync();

    public async Task<string> GetConsentTypeOfApplicationAsync(OpenIddictEntityFrameworkCoreApplication application) => await applicationManager.GetConsentTypeAsync(application);

    public async Task<bool> HasApplicationTheConsentAsync(OpenIddictEntityFrameworkCoreApplication application, string consentType) => await applicationManager.HasConsentTypeAsync(application, consentType);

    /// <summary>
    /// Configures a new <see cref="ClaimsPrincipal"/> with scopes, resources, authorizations and the destinations of each claim
    /// </summary>
    public async Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user, OpenIddictEntityFrameworkCoreApplication application, ImmutableArray<string> scopes, List<object> authorizations)
    {
        var principal = await signInManager.CreateUserPrincipalAsync(user);

        principal.SetScopes(scopes);
        principal.SetResources(await scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

        await CreatePermanentAuthorizationForFutureRequestsWithSameScopes(authorizations, principal, user, application);

        SetTheDestinationOfEachClaim(principal);

        return principal;
    }

    /// <summary>
    /// Automatically create a permanent authorization to avoid requiring explicit consent
    /// for future authorization or token requests containing the same scopes.
    /// </summary>
    /// <returns><see cref="ClaimsPrincipal"/> for which an authorization has been assigned</returns>
    /// <exception cref="Exception">When no authorization could be created or retrieved</exception>
    private async Task CreatePermanentAuthorizationForFutureRequestsWithSameScopes(List<object> authorizations, ClaimsPrincipal principal, User user, OpenIddictEntityFrameworkCoreApplication application)
    {
        var authorization = (authorizations.LastOrDefault() ?? await authorizationManager.CreateAsync(principal: principal,
                                                                                                      subject: user.Id,
                                                                                                      client: application.Id,
                                                                                                      type: AuthorizationTypes.Permanent,
                                                                                                      scopes: principal.GetScopes())) as OpenIddictEntityFrameworkCoreAuthorization;

        if (authorization is null) throw new Exception($"{nameof(authorization)} could not be created or retrieved!");

        principal.SetAuthorizationId(authorization.Id);
    }

    public void SetTheDestinationOfEachClaim(ClaimsPrincipal principal)
    {
        foreach (var claim in principal.Claims)
            claim.SetDestinations(ClaimDestinationManager.DecideDestination(claim, principal));
    }
}