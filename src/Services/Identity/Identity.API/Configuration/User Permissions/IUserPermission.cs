namespace Identity.API.Configuration.User_Permissions;

/// <summary>
/// Defines the operations the user can do using the resource servers
/// </summary>
public interface IUserPermission
{
    public IEnumerable<string> GetAllPermissions();
}