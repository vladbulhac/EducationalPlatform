using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories
{
    public interface IEducationalInstitutionRepository : IRepository<EduInstitution>
    {
        public Task<GetEducationalInstitutionByIDQueryResult> GetByID(Guid EduInstitutionID, CancellationToken cancellationToken);

        public Task<GetEducationalInstitutionByLocationQueryResult> GetByLocation(string locationID, CancellationToken cancellationToken);

        public Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeName(string Name, int offsetValue, int resultsCount, CancellationToken cancellationToken);

        public Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDs(ICollection<Guid> IDs, CancellationToken cancellationToken);
    }
}