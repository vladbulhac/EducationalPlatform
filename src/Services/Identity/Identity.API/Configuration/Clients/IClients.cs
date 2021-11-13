namespace Identity.API.Configuration.Clients;

public interface IClients<TClient> where TClient : class
{
    public IEnumerable<TClient> GetClients();
}