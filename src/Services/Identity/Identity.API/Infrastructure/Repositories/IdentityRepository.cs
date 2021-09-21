using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityContext context;

        public IdentityRepository(IdentityContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task AddEducationalInstitutionAdministratorAsync(EducationalInstitutionAdministrator newAdministrator)
        {
            await context.EducationalInstitutionAdministrators.AddAsync(newAdministrator);
            await context.SaveChangesAsync();
        }

        public async Task<UserPermissions> GetUserPermissionsAsync(string identity) => await context.Permissions.Include(p => p.EducationalInstitutionAdminPermissions)
                                                                                                                .FirstOrDefaultAsync(p => p.UserId == identity);
    }
}