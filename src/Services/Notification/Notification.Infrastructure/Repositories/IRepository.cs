using Notification.Domain.Building_Blocks;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Repositories
{
    /// <summary>
    /// Encapsulates the logic required to access data sources
    /// </summary>
    public interface IRepository<T> where T : IAggregateRoot
    {
        public Task AddAsync(T entity, CancellationToken cancellationToken = default);

        public Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}