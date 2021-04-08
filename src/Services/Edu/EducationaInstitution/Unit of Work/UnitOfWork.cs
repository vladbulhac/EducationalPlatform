using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Unit_of_Work
{
    /// <summary>
    /// Ensures that multiple repositories save changes on the same context
    /// </summary>
    /// <typeparam name="TContext">A class that defines multiple <see cref="DbSet{TEntity}"/> and entities configuration</typeparam>
    public class UnitOfWork : IUnitOfWork //where TContext : DbContext
    {
        private bool disposed;
        private readonly DataContext context;
        public IEducationalInstitutionRepository eduRepository { get; private set; }

        public UnitOfWork()
        {
            context = new();
        }

        /// <returns>Existing instance of <see cref="IEducationalInstitutionRepository"/> or creates a new one</returns>
        public IEducationalInstitutionRepository UseEducationalInstitutionRepository()
        {
            if (eduRepository == null)
                eduRepository = new EducationalInstitutionRepository(context);

            return eduRepository;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed is not true)
            {
                if (disposing is true)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}