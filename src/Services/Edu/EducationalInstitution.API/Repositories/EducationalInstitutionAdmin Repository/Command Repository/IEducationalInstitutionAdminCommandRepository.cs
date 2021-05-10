using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Command_Repository
{
    public interface IEducationalInstitutionAdminCommandRepository
    {
        public Task<bool> DeleteAsync(Guid adminID, Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(ICollection<Guid> adminsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}