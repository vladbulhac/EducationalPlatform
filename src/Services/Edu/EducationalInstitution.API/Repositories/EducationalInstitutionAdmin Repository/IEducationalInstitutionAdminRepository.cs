using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository
{
    public interface IEducationalInstitutionAdminRepository
    {
        public Task<GetAllEducationalInstitutionAdminsQueryResult> GetAllAdminsForEducationalInstitutionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(Guid adminID, Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(ICollection<Guid> adminsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}