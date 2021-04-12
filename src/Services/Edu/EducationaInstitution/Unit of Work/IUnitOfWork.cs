using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Unit_of_Work
{
    /// <summary>
    /// Ensures that multiple repositories save changes on the same context
    /// </summary>
    /// <typeparam name="TContext">A class that defines multiple <see cref="DbSet{TEntity}"/> and entities configuration</typeparam>
    public interface IUnitOfWork : IDisposable
    {
        /// <returns>Existing instance of <see cref="IEducationalInstitutionRepository"/> or creates one and returns it</returns>
        public IEducationalInstitutionRepository UsingEducationalInstitutionRepository();

        /// <returns>Existing instance of <see cref="IEducationalInstitutionBuildingRepository"/> or creates one and returns it</returns>
        public IEducationalInstitutionBuildingRepository UsingEducationalInstitutionBuildingRepository();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}