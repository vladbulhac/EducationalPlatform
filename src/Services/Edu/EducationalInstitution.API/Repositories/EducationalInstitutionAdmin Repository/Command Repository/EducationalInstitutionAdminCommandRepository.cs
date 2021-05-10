using EducationalInstitutionAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Command_Repository
{
    public class EducationalInstitutionAdminCommandRepository : IEducationalInstitutionAdminCommandRepository
    {
        public readonly DataContext context;

        public EducationalInstitutionAdminCommandRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> DeleteAsync(Guid adminID, Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var admin = await context.Admins.Where(a => a.AdminID == adminID && a.EducationalInstitutionID == educationalInstitutionID)
                                     .SingleOrDefaultAsync(cancellationToken);

            if (admin is null) return false;

            context.Remove(admin);
            return true;
        }

        public async Task<bool> DeleteAsync(ICollection<Guid> adminsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var admins = await context.Admins.Where(a => adminsIDs.Contains(a.AdminID) && a.EducationalInstitutionID == educationalInstitutionID)
                                             .ToListAsync(cancellationToken);

            if (admins is null || admins.Count == 0) return false;

            context.RemoveRange(admins);
            return true;
        }
    }
}