﻿using EducationalInstitution.Infrastructure.Repositories.Command_Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work
{
    public interface IUnitOfWorkForCommands : IDisposable
    {
        /// <returns>Returns an instance of a Repository class that implements <see cref="IEducationalInstitutionCommandRepository"/></returns>
        public IEducationalInstitutionCommandRepository UsingEducationalInstitutionCommandRepository();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}