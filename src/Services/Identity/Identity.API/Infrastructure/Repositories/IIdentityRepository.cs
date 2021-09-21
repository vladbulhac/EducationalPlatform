using Identity.API.Models;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure.Repositories
{
    public interface IIdentityRepository
    {
        public Task AddEducationalInstitutionAdministratorAsync(EducationalInstitutionAdministrator admin);

        public Task<UserPermissions> GetUserPermissionsAsync(string identity);
    }
}