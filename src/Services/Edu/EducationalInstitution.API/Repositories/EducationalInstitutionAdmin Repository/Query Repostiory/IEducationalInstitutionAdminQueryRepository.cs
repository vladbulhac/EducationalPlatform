using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository.Query_Repostiory
{
    public interface IEducationalInstitutionAdminQueryRepository
    {
        public Task<GetAllEducationalInstitutionAdminsQueryResult> GetAllAdminsForEducationalInstitutionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}