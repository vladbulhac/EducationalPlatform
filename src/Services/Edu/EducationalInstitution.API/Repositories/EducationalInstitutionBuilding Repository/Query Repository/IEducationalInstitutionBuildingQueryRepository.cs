using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Query_Repository
{
    public interface IEducationalInstitutionBuildingQueryRepository
    {
        /// <summary>
        /// Gets all entities that use the building with the given identifier
        /// </summary>
        /// <param name="buildingID">Building's identifier</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>NULL if the entity has not been found, a record type <see cref="GetAllEducationalInstitutionsWithSameBuildingQueryResult"/> otherwise</returns>
        public Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default);
    }
}