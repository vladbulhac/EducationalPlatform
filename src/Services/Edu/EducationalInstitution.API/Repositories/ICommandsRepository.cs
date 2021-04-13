using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Repositories
{
    /// <summary>
    /// Defines the methods that execute operations of type Create, Update and Delete over a database
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface ICommandsRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Inserts a new <typeparamref name="TEntity"/> entity in the database
        /// </summary>
        /// <param name="data">An object that encapsulates the data to be inserted in the database</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        public Task CreateAsync(TEntity data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the fields of an entity with the given <paramref name="data"/> and saves the changes in the database
        /// </summary>
        /// <param name="data">An object that encapsulates the new values that are used for updating the entity from the database</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> UpdateAsync(TEntity data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="ID">Identifies the data that has to be removed</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and removed, False if the entity has not been found in the database</returns>
        public Task<bool> DeleteAsync(Guid ID, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <typeparamref name="TEntity"/> entity from database based on a unique identifier if it exists in the database
        /// </summary>
        /// <param name="ID">Identifies <typeparamref name="TEntity"/></param>
        /// <param name="cancellationToken">Cancels the operation _____</param>
        /// <returns>The <typeparamref name="TEntity"/> if it has been found, NULL otherwise</returns>
        public Task<TEntity> GetEntityByIDAsync(Guid ID, CancellationToken cancellationToken = default);
    }
}