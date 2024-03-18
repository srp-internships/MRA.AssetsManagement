using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder;

public interface IEntitySeeder
{
    Task Development();
    Task Production();
}

public abstract class EntitySeeder<T> : IEntitySeeder where T : class, IEntity
{
    protected readonly IRepository<T> _repository;

    public EntitySeeder(IRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual Task Development()
    {
        return Task.CompletedTask;
    }

    public virtual Task Production()
    {
        return Task.CompletedTask;
    }
}