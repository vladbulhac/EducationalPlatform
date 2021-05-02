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

namespace EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository
{
    /// <summary>
    /// Contains concrete implementations of the methods that execute Queries and Commands over the <see cref="EducationalInstitution"/> entities
    /// </summary>
    public class EducationalInstitutionRepository : IEducationalInstitutionRepository
    {
        private readonly DataContext context;
        private readonly string dbConnection;

        public EducationalInstitutionRepository(DataContext context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public EducationalInstitutionRepository(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                dbConnection = connectionString;
            else
                dbConnection = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:ConnectionToWriteDB") ?? throw new Exception("No connection string has been found!");
        }

        public async Task CreateAsync(EducationalInstitution data, CancellationToken cancellationToken = default) => await context.EducationalInstitutions.AddAsync(data, cancellationToken);

        public async Task<bool> DeleteAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions.SingleOrDefaultAsync(eduI => eduI.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            context.EducationalInstitutions.Remove(educationalInstitution);
            return true;
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeNameAsync(string name, int offsetValue = 0, int resultsCount = 1, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResult = await connection.QueryAsync<GetEducationalInstitutionQueryResult>
                                                                (@"SELECT EducationalInstitutionID, Name, Description, LocationID
                                                                FROM EducationalInstitutions
                                                                WHERE Name LIKE @Name + '%' AND EntityAccess_IsDisabled=0
                                                                ORDER BY Name
                                                                OFFSET @Offset ROWS
                                                                FETCH NEXT @Results ROWS ONLY",
                                                            new { Name = name, Offset = offsetValue, Results = resultsCount });

                return queryResult.ToList();
            }

            #region Entity Framework Core LINQ

            /*return await context.EducationalInstitutions
                                 .Where(ei => ei.Name.Contains(name) && ei.EntityAccess.IsDisabled==false)
                                 .Select(ei => new GetEducationalInstitutionQueryResult()
                                 {
                                     EducationalInstitutionID = ei.EducationalInstitutionID,
                                     Description = ei.Description,
                                     LocationID = ei.LocationID,
                                     Name = ei.Name
                                 })
                                 .Skip(offsetValue)
                                 .Take(resultsCount)
                                 .OrderBy(ei=>ei.Name)
                                 .ToListAsync(cancellationToken);*/

            #endregion Entity Framework Core LINQ
        }

        public async Task<EducationalInstitution> GetEntityByIDAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<EducationalInstitution>(
                                                                        @"SELECT EducationalInstitutionID, Name, Description, LocationID, JoinDate
                                                                          FROM EducationalInstitutions
                                                                          WHERE EducationalInstitutionID=@ID AND EntityAccess_IsDisabled=0",
                                                                        new { ID = educationalInstitutionID });
            }

            #region Entity Framework Core LINQ

            /*return await context.EducationalInstitutions
                                 .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);*/

            #endregion Entity Framework Core LINQ
        }

        public async Task<GetEducationalInstitutionByIDQueryResult> GetByIDAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResult = await connection.QueryAsync<dynamic>
                                  (@"SELECT e.Name, e.Description, e.LocationID, e.JoinDate, e.ParentInstitutionEducationalInstitutionID as ParentInstitutionID,
                                             ce.EducationalInstitutionID as ChildInstitutionID, ce.Name as ChildInstitutionName, ce.Description as ChildInstitutionDescription,
                                             pe.Name as ParentName, pe.Description as ParentDescription,
                                             b.BuildingID
                                    FROM EducationalInstitutions e
                                    LEFT JOIN EducationalInstitutions ce ON ce.ParentInstitutionEducationalInstitutionID=e.EducationalInstitutionID AND ce.EntityAccess_IsDisabled=0
                                    LEFT JOIN EducationalInstitutions pe ON e.ParentInstitutionEducationalInstitutionID=pe.EducationalInstitutionID AND pe.EntityAccess_IsDisabled=0
                                    LEFT JOIN EducationalInstitutionsBuildings b ON e.EducationalInstitutionID=b.EducationalInstitutionID AND b.EntityAccess_IsDisabled=0
                                    WHERE e.EducationalInstitutionID=@ID AND e.EntityAccess_IsDisabled=0
                                    ORDER BY e.Name, ParentName",
                                    new { ID = educationalInstitutionID });

                return MapQueryResultToGetEducationalInstitutionByIDQueryResult(queryResult.ToList());
            }

            #region Entity Framework Core LINQ

            /*return await context.EducationalInstitutions
                                                 .Where(eduI => eduI.EducationalInstitutionID == educationalInstitutionID && eduI.EntityAccess.IsDisabled == false)
                                                 .Include(ei => ei.Buildings.Where(b=>b.EntityAccess_IsDisabled==false))
                                                 .Include(ei => ei.ChildInstitutions.Where(c=>c.EntityAccess_IsDisabled==false))
                                                 .Include(ei => ei.ParentInstitution.Where(p=>p.EntityAccess_IsDisabled==false))
                                                 .Select(ei => new GetEducationalInstitutionByIDQueryResult()
                                                 {
                                                     BuildingsIDs = ei.Buildings.Select(b => b.BuildingID).ToList(),
                                                     Description = ei.Description,
                                                     Name = ei.Name,
                                                     LocationID = ei.LocationID,
                                                     ChildInstitutions = ei.ChildInstitutions.Select(ci => new EducationalInstitutionBaseQueryResult(ci.EducationalInstitutionID, ci.Name, ci.Description)).ToList(),
                                                     ParentInstitution = new EducationalInstitutionBaseQueryResult(ei.ParentInstitution.EducationalInstitutionID, ei.ParentInstitution.Name, ei.ParentInstitution.Description),
                                                     JoinDate = ei.JoinDate
                                                 })
                                                 .SingleOrDefaultAsync(cancellationToken);*/

            #endregion Entity Framework Core LINQ
        }

        public async Task<GetAllEducationalInstitutionsByLocationQueryResult> GetAllByLocationAsync(string locationID, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(dbConnection))
            {
                await connection.OpenAsync(cancellationToken);

                var queryResult = await connection.QueryAsync<dynamic>(
                                                                    @"SELECT e.EducationalInstitutionID, e.Name, e.Description, b.BuildingID
                                                                       FROM EducationalInstitutions e
                                                                       LEFT JOIN EducationalInstitutionsBuildings b ON e.EducationalInstitutionID=b.EducationalInstitutionID AND b.EntityAccess_IsDisabled=0
                                                                       WHERE e.LocationID=@ID AND e.EntityAccess_IsDisabled=0
                                                                       ORDER BY e.Name",
                                                                    new { ID = locationID });

                return MapQueryResultToGetAllEducationalInstitutionsByLocationQueryResult(queryResult.ToList());
            }

            #region Entity Framework Core LINQ

            /*            return new()
                        {
                            EducationalInstitutions = await context.EducationalInstitutions
                                                                    .Where(ei => ei.LocationID == locationID && ei.EntityAccess.IsDisabled == false)
                                                                    .Include(ei => ei.Buildings.Where(b => b.EntityAccess.IsDisabled == false))
                                                                    .Select(ei => new GetEducationalInstitutionByLocationQueryResult()
                                                                    {
                                                                        Name = ei.Name,
                                                                        BuildingsIDs = ei.Buildings.Select(b => b.BuildingID).ToList(),
                                                                        Description = ei.Description,
                                                                        EducationalInstitutionID = ei.EducationalInstitutionID
                                                                    })
                                                                  .ToListAsync(cancellationToken)
                        };*/

            #endregion Entity Framework Core LINQ
        }

        public async Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDsAsync(ICollection<Guid> IDs, CancellationToken cancellationToken = default)
        {
            return await context.EducationalInstitutions
                                 .Where(eduI => IDs.Contains(eduI.EducationalInstitutionID))
                                 .Select(ei => new GetEducationalInstitutionQueryResult()
                                 {
                                     EducationalInstitutionID = ei.EducationalInstitutionID,
                                     LocationID = ei.LocationID,
                                     Name = ei.Name,
                                     Description = ei.Description
                                 })
                                 .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(EducationalInstitution data, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                      .SingleOrDefaultAsync(eduI => eduI.EducationalInstitutionID == data.EducationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.Update(data.Name, data.Description, data.LocationID);
            return true;
        }

        public async Task<bool> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateEntireLocation(locationID, addBuildingsIDs, removeBuildingsIDs);
            return true;
        }

        public async Task<bool> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                     .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateLocation(locationID);
            return true;
        }

        public async Task<bool> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.CreateAndAddBuildings(addBuildingsIDs);
            educationalInstitution.RemoveBuildings(removeBuildingsIDs);
            return true;
        }

        public async Task<bool> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateName(name);
            return true;
        }

        public async Task<bool> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                    .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateDescription(description);
            return true;
        }

        public async Task<bool> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.Update(name, description);
            return true;
        }

        public async Task<bool> UpdateParentInstitutionAsync(Guid educationalInstitutionID, EducationalInstitution parentInstitution, CancellationToken cancellationToken = default)
        {
            var educationalInstitution = await context.EducationalInstitutions
                                                        .SingleOrDefaultAsync(ei => ei.EducationalInstitutionID == educationalInstitutionID, cancellationToken);

            if (educationalInstitution is null) return false;

            educationalInstitution.UpdateParentInstitution(parentInstitution);
            return true;
        }

        #region GetAllByLocationAsync() query result map methods

        private static GetAllEducationalInstitutionsByLocationQueryResult MapQueryResultToGetAllEducationalInstitutionsByLocationQueryResult(IList<dynamic> queryResult)
        {
            if (queryResult.Count == 0) return null;

            var idToRecordMap = new Dictionary<Guid, GetEducationalInstitutionByLocationQueryResult>(queryResult.Count);
            for (int entityIndex = 0; entityIndex < queryResult.Count; entityIndex++)
            {
                var educationalInstitutionID = (Guid)queryResult[entityIndex].EducationalInstitutionID;

                if (!idToRecordMap.ContainsKey(educationalInstitutionID))
                {
                    var result = MapEntityToGetEducationalInstitutionByLocationQueryResult(queryResult[entityIndex], educationalInstitutionID);
                    idToRecordMap.Add(educationalInstitutionID, result);
                }
                else
                    idToRecordMap[educationalInstitutionID].BuildingsIDs.Add((string)queryResult[entityIndex].BuildingID);
            }

            return new() { EducationalInstitutions = idToRecordMap.Select(qr => qr.Value).ToList() };
        }

        private static GetEducationalInstitutionByLocationQueryResult MapEntityToGetEducationalInstitutionByLocationQueryResult(dynamic queryResultItem, Guid educationalInstitutionID)
        {
            var result = new GetEducationalInstitutionByLocationQueryResult()
            {
                EducationalInstitutionID = educationalInstitutionID,
                Name = (string)queryResultItem.Name,
                Description = (string)queryResultItem.Description,
                BuildingsIDs = new HashSet<string>()
            };

            var buildingID = queryResultItem.BuildingID as string;
            if (buildingID is not null && !result.BuildingsIDs.Contains(buildingID))
                result.BuildingsIDs.Add(buildingID);

            return result;
        }

        #endregion GetAllByLocationAsync() query result map methods

        #region GetByIDAsync() query result map methods

        private static GetEducationalInstitutionByIDQueryResult MapQueryResultToGetEducationalInstitutionByIDQueryResult(IList<dynamic> queryResult)
        {
            if (queryResult.Count == 0) return null;

            HashSet<string> buildings = new(queryResult.Count);
            HashSet<EducationalInstitutionBaseQueryResult> childInstitutions = new(queryResult.Count);

            MapQueryResultBuildingsAndChildInstitutions(queryResult, ref buildings, ref childInstitutions);

            return new()
            {
                Name = (string)queryResult[0].Name,
                Description = (string)queryResult[0].Description,
                LocationID = (string)queryResult[0].LocationID,
                JoinDate = (DateTime)queryResult[0].JoinDate,
                ParentInstitution = MapQueryResultParentInstitution(queryResult[0]),
                ChildInstitutions = childInstitutions,
                BuildingsIDs = buildings
            };
        }

        private static void MapQueryResultBuildingsAndChildInstitutions(IList<dynamic> queryResult, ref HashSet<string> buildings, ref HashSet<EducationalInstitutionBaseQueryResult> childInstitutions)
        {
            foreach (var result in queryResult)
            {
                var buildingID = result.BuildingID as string;
                if (buildingID is not null && !buildings.Contains(buildingID))
                    buildings.Add((string)result.BuildingID);

                var childInstitutionID = result.ChildInstitutionID as Guid?;
                if (childInstitutionID is not null)
                {
                    EducationalInstitutionBaseQueryResult childInstitution = new((Guid)childInstitutionID, (string)result.ChildInstitutionName, (string)result.ChildInstitutionDescription);
                    if (!childInstitutions.Contains(childInstitution))
                        childInstitutions.Add(childInstitution);
                }
            }
        }

        private static EducationalInstitutionBaseQueryResult MapQueryResultParentInstitution(dynamic queryResultItem)
        {
            var parentInstitutionID = queryResultItem.ParentInstitutionID as Guid?;
            EducationalInstitutionBaseQueryResult parentInstitution = null;

            if (parentInstitutionID is not null)
                parentInstitution = new((Guid)parentInstitutionID, (string)queryResultItem.ParentName, (string)queryResultItem.ParentDescription);

            return parentInstitution;
        }

        #endregion GetByIDAsync() query result map methods
    }
}