using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Command_Repository
{
    public interface IEducationalInstitutionBuildingCommandRepository
    {
        public Task<bool> DeleteAsync(string buildingID, Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(ICollection<string> buildingsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}