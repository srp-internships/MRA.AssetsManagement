using System.Linq.Expressions;

using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Application.Abstractions;

public interface IRepository<T> where T : IEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(string id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task CreateAsync(T entity);
    Task RemoveAsync(string id);
    Task UpdateAsync(T entity);
}