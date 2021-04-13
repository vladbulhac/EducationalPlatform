using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories
{
    /// <summary>
    /// Defines specific operations over the set of <see cref="EducationalInstitution"/> entities
    /// </summary>
    public interface IEducationalInstitutionRepository : ICommandsRepository<EducationalInstitution>
    {
        /// <summary>
        /// Gets an entity, including its related entities, based on a unique identifier if it exists in the database
        /// </summary>
        /// <param name="EduInstitutionID">Entity's identifier</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>NULL if the entity has not been found, a record type <see cref="GetEducationalInstitutionByIDQueryResult"/> otherwise</returns>
        public Task<GetEducationalInstitutionByIDQueryResult> GetByIDAsync(Guid EduInstitutionID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an entity based on a location identifier if it exists in the database
        /// </summary>
        /// <param name="locationID">Location's identifier</param>
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
        public Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeNameAsync(string name, int offsetValue, int resultsCount, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a collection of entities that have a unique identifier in <paramref name="IDs"/>
        /// </summary>
        /// <param name="IDs">List of identifiers based on which the lookup is made</param>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>NULL if no entities have been found, a collection of records of type <see cref="GetEducationalInstitutionQueryResult"/> otherwise</returns>
        public Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDsAsync(ICollection<Guid> IDs, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the <paramref name="locationID"/> and <paramref name="buildingID"/> fields of an entity and saves the changes in the database
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateAsync(Guid eduInstituionID, string locationID, ICollection<string> buildingsIDs, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the <paramref name="locationID"/> field of an entity and saves the changes in the database
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateAsync(Guid eduInstitutionID, string locationID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the <paramref name="buildingsIDs"/> field of an entity and saves the changes in the database
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateAsync(Guid eduInstitutionID, ICollection<string> buildingsIDs, CancellationToken cancellationToken = default);
    }
}