using Dapper;
using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute Queries and Commands over the <see cref="EducationalInstitutionBuilding"/> entities
    /// </summary>
    public class EducationalInstitutionBuildingRepository : IEducationalInstitutionBuildingRepository
    {
        private readonly DataContext context;
        private readonly string dbConnection;

        public EducationalInstitutionBuildingRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public EducationalInstitutionBuildingRepository(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                dbConnection = connectionString;
            else
                dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<bool> DeleteAsync(string buildingID, CancellationToken cancellationToken = default)
        {
            var educationalInstitutionBuilding = await context.EducationalInstitutionsBuildings
                                                                .SingleOrDefaultAsync(eib => eib.BuildingID == buildingID, cancellationToken);

            if (educationalInstitutionBuilding is null) return false;

            context.EducationalInstitutionsBuildings.Remove(educationalInstitutionBuilding);
            return true;
        }

        public async Task<bool> DeleteAsync(ICollection<string> buildingsIDs, CancellationToken cancellationToken = default)
        {
            var educationalInstitutionBuildings = await context.EducationalInstitutionsBuildings
                                                              .Where(eib => buildingsIDs.Contains(eib.BuildingID)).ToListAsync(cancellationToken);

            if (educationalInstitutionBuildings is null || educationalInstitutionBuildings.Count == 0) return false;

            for (int buildingIndex = 0; buildingIndex < educationalInstitutionBuildings.Count; buildingIndex++)
                context.EducationalInstitutionsBuildings.Remove(educationalInstitutionBuildings[buildingIndex]);

            return true;
        }

        public async Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResult = await connection.QueryAsync<EducationalInstitutionBaseQueryResult>(@"
                                                                       SELECT e.EducationalInstitutionID, e.Name, e.Description
                                                                       FROM EducationalInstitutionsBuildings b
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