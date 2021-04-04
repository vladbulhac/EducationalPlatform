using EducationaInstitutionAPI.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Unit_of_Work
{
    public interface IUnitOfWork : IDisposable
    {
        public IEducationalInstitutionRepository UseEducationalInstitutionRepository();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}