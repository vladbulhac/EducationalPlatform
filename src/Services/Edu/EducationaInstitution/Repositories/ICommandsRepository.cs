using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories
{
    /// <summary>
    /// Defines the methods that execute operations of type Create, Update and Delete
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface ICommandsRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Inserts  a new <typeparamref name="TEntity"/> entity in the database
        /// </summary>
        /// <param name="data">An object that encapsulates the data to be inserted in the database</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        public Task Create(TEntity data, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the fields of an entity with the given <paramref name="data"/> and saves the changes in the database
        /// </summary>
        /// <param name="data">An object that encapsulates the new values that are used for updating the entity from the database</param>
        /// <param name="cancellationToken">Cancels the operation ________</param>
        /// <returns>True if the entity has been found and updated, False if the entity has not been found in the database</returns>
        public Task<bool> Update(TEntity data, CancellationToken cancellationToken);

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="ID">Identifies the data that has to be removed</param>
        /// <param name="cancellationToken">Cancels the operation _______</param>
        /// <returns>True if the entity has been found and removed, False if the entity has not been found in the database</returns>
        public Task<bool> Delete(Guid ID, CancellationToken cancellationToken);
    }
}