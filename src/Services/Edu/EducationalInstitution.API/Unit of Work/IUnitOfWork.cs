using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Unit_of_Work
{
    /// <summary>
    /// Ensures that multiple repositories save changes on the same context
    /// </summary>
    /// <typeparam name="TContext">A class that defines multiple <see cref="DbSet{TEntity}"/> and entities configuration</typeparam>
    public interface IUnitOfWork : IDisposable
    {
        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionRepository"/></returns>
        public IEducationalInstitutionRepository UsingEducationalInstitutionRepository();

        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionBuildingRepository"/></returns>
        public IEducationalInstitutionBuildingRepository UsingEducationalInstitutionBuildingRepository();

        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionAdminRepository"/></returns>
        public IEducationalInstitutionAdminRepository UsingEducationalInstitutionAdminRepository();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}