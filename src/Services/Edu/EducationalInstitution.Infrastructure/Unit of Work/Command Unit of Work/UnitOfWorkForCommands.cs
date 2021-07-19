﻿using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work
{
    public class UnitOfWorkForCommands : IUnitOfWorkForCommands
    {
        private bool disposed;
        private readonly DataContext context;

        public IEducationalInstitutionCommandRepository EducationalInstitutionRepository { get; private set; }

        public UnitOfWorkForCommands(DbContextOptions<DataContext> options) => context = new(options);

        public IEducationalInstitutionCommandRepository UsingEducationalInstitutionCommandRepository()
        {
            if (EducationalInstitutionRepository is null)
                EducationalInstitutionRepository = new EducationalInstitutionCommandRepository(context);

            return EducationalInstitutionRepository;
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