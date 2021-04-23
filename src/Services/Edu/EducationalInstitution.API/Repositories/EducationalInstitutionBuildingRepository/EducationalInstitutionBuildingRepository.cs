using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using EducationalInstitutionAPI.Utils;
using Microsoft.Data.SqlClient;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute Queries and Commands over the <see cref="EducationalInstitutionBuilding"/> entities
    /// </summary>
    public class EducationalInstitutionBuildingRepository : IEducationalInstitutionBuildingRepository
    {
        private readonly DataContext context;
        private readonly string dbConnection;

        public EducationalInstitutionBuildingRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public EducationalInstitutionBuildingRepository()
        {
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

            if (educationalInstitutionBuildings is null || educationalInstitutionBuildings.Count is 0) return false;

            for (int buildingIndex = 0; buildingIndex < educationalInstitutionBuildings.Count; buildingIndex++)
                context.EducationalInstitutionsBuildings.Remove(educationalInstitutionBuildings[buildingIndex]);

            return true;
        }

        public async Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection(dbConnection))
            {
                connection.Open();

                var queryResult = await connection.QueryAsync<dynamic>(@"SELECT e.EducationalInstitutionID, e.Name, e.Description
                                                                       FROM dbo.EducationalInstitutionsBuildings b
                                                                       JOIN dbo.EducationalInstitutions e
                                                                       ON b.EducationalInstitutionID=e.EducationalInstitutionID
                                                                       WHERE b.BuildingID=@BuildingID AND b.EntityAccess_IsDisabled=0",
                                                                        new { buildingID });

                GetAllEducationalInstitutionsWithSameBuildingQueryResult result = new() { EducationalInstitutions = new List<EducationalInstitutionEssentialData>() };

                foreach (var building in queryResult)
                {
                    result.EducationalInstitutions.Add(
                                                    new()
                                                    {
                                                        EducationalInstitutionID = (Guid)building.EducationalInstitutionID,
                                                        Name = (string)building.Name,
                                                        Description = (string)building.Description
                                                    }
                                                    );
                }

                return result;
            }
            /* return new()
             {
                 EducationalInstitutions = await context.EducationalInstitutionsBuildings
                                                                         .Where(eib => eib.BuildingID == buildingID)
                                                                         .Include(ei => ei.EducationalInstitution)
                                                                         .Select(s => new EducationalInstitutionEssentialData()
                                                                         {
                                                                             EducationalInstitutionID = s.EducationalInstitution.EducationalInstitutionID,
                                                                             Name = s.EducationalInstitution.Name,
                                                                             Description = s.EducationalInstitution.Description
                                                                         })
                                                                         .ToListAsync(cancellationToken)
             };*/
        }
    }
}