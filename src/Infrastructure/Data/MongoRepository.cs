using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Infrastructure.Data;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _collection;
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_filterBuilder.Empty).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter,CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(e => e.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateAsync(CancellationToken cancellationToken, params T[] entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await _collection.InsertManyAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        FilterDefinition<T> filter = _filterBuilder.Eq(e => e.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }
    
    public Task<bool> Any(Expression<Func<T, bool>>? filter = default, CancellationToken cancellationToken = default)
    {
        return filter is null ? _collection.AsQueryable().AnyAsync() : _collection.AsQueryable().AnyAsync(filter, cancellationToken);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(e => e.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }
}