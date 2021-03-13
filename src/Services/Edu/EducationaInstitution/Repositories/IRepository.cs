using System;
using System.Threading;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Repositories
{
    public interface IRepository<T>
    {
        public Task<Guid> Create(T data, CancellationToken cancellationToken);
        public Task<bool> Update(T data, CancellationToken cancellationToken);
        public Task<bool> Delete(Guid ID, CancellationToken cancellationToken);
    }
}