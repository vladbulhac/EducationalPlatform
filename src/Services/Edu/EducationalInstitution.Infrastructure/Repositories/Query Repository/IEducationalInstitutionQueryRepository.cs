using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository;

/// <summary>
/// Contains methods that are used in retrieving data from a data source
/// </summary>
public interface IEducationalInstitutionQueryRepository : IRepository<Domain::EducationalInstitution>
{
    /// <summary>
    /// Gets an entity, including its related entities, based on a unique identifier if it exists in the database
    /// </summary>
    /// <param name="cancellationToken">Cancels the operation _______</param>
    /// <returns>NULL if the entity has not been found, a record type <see cref="GetEducationalInstitutionByIDQueryResult"/> otherwise</returns>
    public Task<GetEducationalInstitutionByIDQueryResult> GetByIDAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);

    /// <param name="cancellationToken">Cancels the operation ______</param>
    /// <returns>NULL if the entity has not been found, a record type <see cref="GetAllEducationalInstitutionsByLocationQueryResult"/> otherwise</returns>
    public Task<GetAllEducationalInstitutionsByLocationQueryResult> GetAllByLocationAsync(string locationID, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a collection with at most <paramref name="resultsCount"/> entities that contain <paramref name="name"/>
    /// </summary>
    /// <param name="name">Text based on which the lookup is made</param>
    /// <param name="offsetValue">Skips a specified number of results</param>
    /// <param name="resultsCount">Number of results to be fetched</param>
    /// <param name="cancellationToken">Cancels the operation ______</param>
    /// <returns>NULL if no entities have been found, a collection of records of type <see cref="GetEducationalInstitutionQueryResult"/> otherwise</returns>
    public Task<GetAllEducationalInstitutionsByNameQueryResult> GetAllByNameAsync(string name, int offsetValue, int resultsCount, CancellationToken cancellationToken = default);

    /// <param name="cancellationToken">Cancels the operation ______</param>
    /// <returns>NULL if the entity has not been found, a record type <see cref="GetAllEducationalInstitutionsWithSameBuildingQueryResult"/> otherwise</returns>
    public Task<GetAllEducationalInstitutionsWithSameBuildingQueryResult> GetAllEducationalInstitutionsWithSameBuildingAsync(string buildingID, CancellationToken cancellationToken = default);

    /// <param name="cancellationToken">Cancels the operation ______</param>
    /// <returns>NULL if the entity has not been found, a record type <see cref="GetAllEducationalInstitutionAdminsQueryResult"/> otherwise </returns>
    public Task<GetAllAdminsOfEducationalInstitutionQueryResult> GetAllAdminsForEducationalInstitutionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);
}