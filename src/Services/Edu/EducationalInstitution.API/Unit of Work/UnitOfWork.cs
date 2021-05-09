using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Repositories.EducationalInstitution_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionAdmin_Repository;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionBuilding_Repository;
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
        public IEducationalInstitutionBuildingRepository BuildingRepository { get; private set; }
        public IEducationalInstitutionAdminRepository AdminRepository { get; private set; }

        public UnitOfWork(DbContextOptions<DataContext> options) => context = new(options);

        public IEducationalInstitutionRepository UsingEducationalInstitutionRepository()
        {
            if (EducationalInstitutionRepository is null)
                EducationalInstitutionRepository = new EducationalInstitutionRepository(context, null);

            return EducationalInstitutionRepository;
        }

        public IEducationalInstitutionBuildingRepository UsingEducationalInstitutionBuildingRepository()
        {
            if (BuildingRepository is null)
                BuildingRepository = new EducationalInstitutionBuildingRepository(context, null);

            return BuildingRepository;
        }

        public IEducationalInstitutionAdminRepository UsingEducationalInstitutionAdminRepository()
        {
            if (AdminRepository is null)
                AdminRepository = new EducationalInstitutionAdminRepository(context, null);

            return AdminRepository;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
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