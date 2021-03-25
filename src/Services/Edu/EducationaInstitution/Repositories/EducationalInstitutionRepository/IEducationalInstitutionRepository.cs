using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories
{
    /// <summary>
    /// Defines the specific operations over the set of Educational Institution entities
    /// </summary>
    public interface IEducationalInstitutionRepository : ICommandsRepository<EduInstitution>
    {
        /// <summary>
        /// Gets an entity based on a unique identifier if it exists in the database
        /// </summary>
        /// <param name="EduInstitutionID">Entity's identifier</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>NULL if the entity has not been found, a record type GetEducationalInstitutionByIDQueryResult otherwise</returns>
        public Task<GetEducationalInstitutionByIDQueryResult> GetByID(Guid EduInstitutionID, CancellationToken cancellationToken);

        /// <summary>
        /// Gets an entity based on a location identifier if it exists in the database
        /// </summary>
        /// <param name="locationID">Location's identifier</param>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>NULL if the entity has not been found, a record type GetEducationalInstitutionByLocationQueryResult otherwise</returns>
        public Task<GetEducationalInstitutionByLocationQueryResult> GetByLocation(string locationID, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a collection with at most <paramref name="resultsCount"/> entities that contain <paramref name="name"/>
        /// </summary>
        /// <param name="name">Text based on which the lookup is made</param>
        /// <param name="offsetValue">Skips a specified number of results</param>
        /// <param name="resultsCount">Number of results to be fetched</param>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>NULL if no entities have been found, a collection of records of type GetEducationalInstitutionQueryResult otherwise</returns>
        public Task<ICollection<GetEducationalInstitutionQueryResult>> GetAllLikeName(string name, int offsetValue, int resultsCount, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a collection of entities that have a unique identifier in <paramref name="IDs"/>
        /// </summary>
        /// <param name="IDs">List of identifiers based on which the lookup is made</param>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>NULL if no entities have been found, a collection of records of type GetEducationalInstitutionQueryResult otherwise</returns>
        public Task<ICollection<GetEducationalInstitutionQueryResult>> GetFromCollectionOfIDs(ICollection<Guid> IDs, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the <paramref name="locationID"/> and <paramref name="buildingID"/> fields of an entity and saves the changes in the database
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> Update(Guid eduInstituionID, string locationID, string buildingID, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the <paramref name="locationID"/> field of an entity and saves the changes in the database
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> Update(Guid eduInstitutionID, string locationID, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the <paramref name="buildingID"/> field of an entity and saves the changes in the database
        /// </summary>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> Update(string buildingID, Guid eduInstitutionID, CancellationToken cancellationToken);
    }
}