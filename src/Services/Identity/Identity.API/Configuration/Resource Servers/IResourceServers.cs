namespace Identity.API.Configuration.Resource_Servers;

/// <summary>
/// Resource servers contain information that clients are interested in
/// </summary>
public interface IResourceServers<TResourceServer> where TResourceServer : class
{
    public IEnumerable<TResourceServer> GetResourceServers();
}