using Notification.Domain.Building_Blocks;

namespace Notification.Infrastructure.Repositories
{
    /// <summary>
    /// Encapsulates the logic required to access data sources
    /// </summary>
    public interface IRepository<T> where T : IAggregateRoot
    { }
}