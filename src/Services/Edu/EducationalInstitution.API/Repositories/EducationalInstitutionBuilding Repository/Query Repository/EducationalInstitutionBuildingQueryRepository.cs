using Dapper;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository.Query_Repository
{
    public class EducationalInstitutionBuildingQueryRepository : IEducationalInstitutionBuildingQueryRepository
    {
        private readonly string dbConnection;

        public EducationalInstitutionBuildingQueryRepository(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                dbConnection = connectionString;
            else
                dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResult = await connection.QueryAsync<EducationalInstitutionBaseQueryResult>(@"
                                                                       SELECT e.EducationalInstitutionID, e.Name, e.Description
                                                                       FROM Buildings b
                                                                       JOIN EducationalInstitutions e ON b.EducationalInstitutionID=e.EducationalInstitutionID
                                                                       WHERE b.BuildingID=@ID AND b.EntityAccess_IsDisabled=0 AND e.EntityAccess_IsDisabled=0
                                                                       ORDER BY e.Name",
                                                                       new { ID = buildingID });

                return new() { EducationalInstitutions = queryResult.ToList() };
            }

            #region Entity Framework Core LINQ

            /*return new()
            {
                EducationalInstitutions = await context.EducationalInstitutionsBuildings
                                                                        .Where(eib => eib.BuildingID == buildingID)
                                                                        .Include(ei => ei.EducationalInstitution.Where(e=>e.EntityAccess_IsDisabled==false))
                                                                        .Select(s => new EducationalInstitutionEssentialData()
                                                                        {
                                                                            EducationalInstitutionID = s.EducationalInstitution.EducationalInstitutionID,
                                                                            Name = s.EducationalInstitution.Name,
                                                                            Description = s.EducationalInstitution.Description
                                                                        })
                                                                        .OrderBy(ei=>ei.Name)
                                                                        .ToListAsync(cancellationToken)
            };*/

            #endregion Entity Framework Core LINQ
        }
    }
}