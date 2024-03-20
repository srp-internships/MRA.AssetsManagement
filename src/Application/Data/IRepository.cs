using System.Linq.Expressions;

using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Application.Data;

public interface IRepository<T> where T : IEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<T> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task CreateAsync(CancellationToken cancellationToken = default, params T[] entity);
    Task RemoveAsync(string id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> Any(Expression<Func<T, bool>>? filter = default, CancellationToken cancellationToken = default);
}