using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository
{
    /// <summary>
    /// Defines specific operations over the set of <see cref="EducationalInstitutionBuilding"/> entities
    /// </summary>
    public interface IEducationalInstitutionBuildingRepository
    {
        /// <summary>
        /// Gets all entities that use the building with the given identifier
        /// </summary>
        /// <param name="buildingID">Building's identifier</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>NULL if the entity has not been found, a record type <see cref="GetAllEducationalInstitutionsWithSameBuildingQueryResult"/> otherwise</returns>
        public Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(string buildingID, Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        public Task<bool> DeleteAsync(ICollection<string> buildingsIDs, Guid educationalInstitutionID, CancellationToken cancellationToken = default);
    }
}