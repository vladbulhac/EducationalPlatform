using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Repositories.EducationalInstitutionBuildingRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Unit_of_Work
{
    public class UnitOfWork : IUnitOfWork //where TContext : DbContext
    {
        private bool disposed;
        private readonly DataContext context;
        public IEducationalInstitutionRepository EduRepository { get; private set; }
        public IEducationalInstitutionBuildingRepository EduBuildingRepository { get; private set; }

        public UnitOfWork() => context = new();

        public IEducationalInstitutionRepository UsingEducationalInstitutionRepository()
        {
            if (EduRepository is null)
                EduRepository = new EducationalInstitutionRepository(context);

            return EduRepository;
        }

        public IEducationalInstitutionBuildingRepository UsingEducationalInstitutionBuildingRepository()
        {
            if (EduBuildingRepository is null)
                EduBuildingRepository = new EducationalInstitutionBuildingRepository(context);

            return EduBuildingRepository;
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