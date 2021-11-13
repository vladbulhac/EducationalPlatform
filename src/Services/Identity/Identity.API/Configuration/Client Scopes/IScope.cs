using OpenIddict.Abstractions;

namespace Identity.API.Configuration.Client_Scopes;

/// <summary>
/// Defines the operations the client can do on behalf of the user/resource owner
/// </summary>
public interface IScope
{
    /// <summary>
    /// Maps scopes to the resources it can access
    /// </summary>
    public Task RegisterScopes(IOpenIddictScopeManager scopeManager);

    public IEnumerable<string> GetAllScopes();
}