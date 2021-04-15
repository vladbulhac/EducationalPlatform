using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Unit_of_Work
{
    public class UnitOfWork : IUnitOfWork //where TContext : DbContext
    {
        private bool disposed;
        private readonly DataContext context;
        public IEducationalInstitutionRepository EducationalInstitutionRepository { get; private set; }
        public IEducationalInstitutionBuildingRepository EducationalInstitutionBuildingRepository { get; private set; }

        public UnitOfWork(DbContextOptions<DataContext> options) => context = new(options);

        public IEducationalInstitutionRepository UsingEducationalInstitutionRepository()
        {
            if (EducationalInstitutionRepository is null)
                EducationalInstitutionRepository = new EducationalInstitutionRepository(context);

            return EducationalInstitutionRepository;
        }

        public IEducationalInstitutionBuildingRepository UsingEducationalInstitutionBuildingRepository()
        {
            if (EducationalInstitutionBuildingRepository is null)
                EducationalInstitutionBuildingRepository = new EducationalInstitutionBuildingRepository(context);

            return EducationalInstitutionBuildingRepository;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);

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