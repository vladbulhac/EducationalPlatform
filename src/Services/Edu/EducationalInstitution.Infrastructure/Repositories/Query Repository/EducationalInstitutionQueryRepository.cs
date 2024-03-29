﻿using Dapper;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using System.Data.SqlClient;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository;

/// <inheritdoc cref="IEducationalInstitutionQueryRepository"/>
public class EducationalInstitutionQueryRepository : IEducationalInstitutionQueryRepository
{
    private readonly string dbConnection;

    /// <exception cref="Exception"/>
    public EducationalInstitutionQueryRepository(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        dbConnection = connectionString;
    }

    public async Task<GetAllEducationalInstitutionsByNameQueryResult> GetAllByNameAsync(string name, int offsetValue = 0, int resultsCount = 1, CancellationToken cancellationToken = default)
    {
        await using (var connection = new SqlConnection(dbConnection))
        {
            await connection.OpenAsync(cancellationToken);

            var queryResult = await connection.QueryAsync<GetEducationalInstitutionQueryResult>
                                                            (@"SELECT EducationalInstitutionID, Name, Description, LocationID
                                                                FROM EducationalInstitutions
                                                                WHERE Name LIKE @Name + '%' AND IsDisabled=0
                                                                ORDER BY Name
                                                                OFFSET @Offset ROWS
                                                                FETCH NEXT @Results ROWS ONLY",
                                                        new { Name = name, Offset = offsetValue, Results = resultsCount });

            return new() { EducationalInstitutions = queryResult.ToList() };
        }
    }

    public async Task<GetEducationalInstitutionByIDQueryResult> GetByIDAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
    {
        await using (var connection = new SqlConnection(dbConnection))
        {
            await connection.OpenAsync(cancellationToken);

            var queryResult = await connection.QueryAsync<dynamic>
                              (@"SELECT e.Name, e.Description, e.LocationID, e.JoinDate, e.ParentInstitutionId,
                                             ce.EducationalInstitutionID as ChildInstitutionID, ce.Name as ChildInstitutionName, ce.Description as ChildInstitutionDescription,
                                             pe.Name as ParentName, pe.Description as ParentDescription,
                                             b.Id as BuildingID
                                    FROM EducationalInstitutions e
                                    LEFT JOIN EducationalInstitutions ce ON ce.ParentInstitutionId=e.EducationalInstitutionID AND ce.IsDisabled=0
                                    LEFT JOIN EducationalInstitutions pe ON e.ParentInstitutionId=pe.EducationalInstitutionID AND pe.IsDisabled=0
                                    LEFT JOIN Buildings b ON e.EducationalInstitutionID=b.EducationalInstitutionID AND b.IsDisabled=0
                                    WHERE e.EducationalInstitutionID=@ID AND e.IsDisabled=0
                                    ORDER BY e.Name, ParentName",
                                new { ID = educationalInstitutionID });

            return MapQueryResultToGetEducationalInstitutionByIDQueryResult(queryResult.ToList());
        }
    }

    public async Task<GetAllEducationalInstitutionsByLocationQueryResult> GetAllByLocationAsync(string locationID, CancellationToken cancellationToken = default)
    {
        await using (var connection = new SqlConnection(dbConnection))
        {
            await connection.OpenAsync(cancellationToken);

            var queryResult = await connection.QueryAsync<dynamic>
                                                                (@"SELECT e.EducationalInstitutionID, e.Name, e.Description, b.Id as BuildingID
                                                                       FROM EducationalInstitutions e
                                                                       LEFT JOIN Buildings b ON e.EducationalInstitutionID=b.EducationalInstitutionID AND b.IsDisabled=0
                                                                       WHERE e.LocationID=@ID AND e.IsDisabled=0
                                                                       ORDER BY e.Name",
                                                                new { ID = locationID });

            return MapQueryResultToGetAllEducationalInstitutionsByLocationQueryResult(queryResult.ToList());
        }
    }

    public async Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default)
    {
        await using (var connection = new SqlConnection(dbConnection))
        {
            await connection.OpenAsync(cancellationToken);

            var queryResult = await connection.QueryAsync<EducationalInstitutionBaseQueryResult>
                                                                (@"SELECT e.EducationalInstitutionID, e.Name, e.Description
                                                                       FROM Buildings b
                                                                       JOIN EducationalInstitutions e ON b.EducationalInstitutionID=e.EducationalInstitutionID
                                                                       WHERE b.Id=@ID AND b.IsDisabled=0 AND e.IsDisabled=0
                                                                       ORDER BY e.Name",
                                                                   new { ID = buildingID });

            return new() { EducationalInstitutions = queryResult.ToList() };
        }
    }

    public async Task<GetAllAdminsOfEducationalInstitutionQueryResult> GetAllAdminsForEducationalInstitutionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default)
    {
        await using (var connection = new SqlConnection(dbConnection))
        {
            await connection.OpenAsync(cancellationToken);

            var queryResult = await connection.QueryAsync<string>
                                                         (@"SELECT Id as AdminID
                                                                FROM Admins
                                                                WHERE EducationalInstitutionID=@ID AND IsDisabled=0",
                                                            new { ID = educationalInstitutionID });

            return new() { AdminsIDs = queryResult.ToList() };
        }
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
            JoinDate = ((DateTime)queryResult[0].JoinDate).ToUniversalTime(),
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
        var parentInstitutionID = queryResultItem.ParentInstitutionId as Guid?;
        EducationalInstitutionBaseQueryResult parentInstitution = null;

        if (parentInstitutionID is not null)
            parentInstitution = new((Guid)parentInstitutionID, (string)queryResultItem.ParentName, (string)queryResultItem.ParentDescription);

        return parentInstitution;
    }

    #endregion GetByIDAsync() query result map methods
}