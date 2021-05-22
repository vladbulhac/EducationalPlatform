using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Repositories_results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository.Command_Repository
{
    /// <summary>
    /// Defines specific command operations over the set of <see cref="EducationalInstitution"/> entities
    /// </summary>
    public interface IEducationalInstitutionCommandRepository
    {
        /// <summary>
        /// Inserts a new <see cref="EducationalInstitution"/> entity in the database
        /// </summary>
        /// <param name="data">An object that encapsulates the data to be inserted in the database</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        public Task CreateAsync(EducationalInstitution data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="ID">Identifies the data that has to be removed</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and removed, False if the entity has not been found in the database</returns>
        public Task<bool> DeleteAsync(Guid ID, CancellationToken cancellationToken = default);

        /// <returns>An empty collection if the entity has not been found, otherwise a collection of admin ids that are notified of the event</returns>
        public Task<DeleteCommandRepositoryResult> ScheduleForDeletionAsync(Guid educationalInstitutionID, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateEntireLocationAsync(Guid educationalInstitutionID, string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateLocationAsync(Guid educationalInstitutionID, string locationID, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateBuildingsAsync(Guid educationalInstitutionID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation _________</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<CommandRepositoryResult> UpdateNameAsync(Guid educationalInstitutionID, string name, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<CommandRepositoryResult> UpdateDescriptionAsync(Guid educationalInstitutionID, string description, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation ______</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<CommandRepositoryResult> UpdateNameAndDescriptionAsync(Guid educationalInstitutionID, string name, string description, CancellationToken cancellationToken = default);

        /// <param name="cancellationToken">Cancels the operation ____________</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateParentInstitutionAsync(Guid educationalInstitutionID, EducationalInstitution parentInstitution, CancellationToken cancellationToken = default);
    }
}