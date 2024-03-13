using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Abstractions;

public interface IApplicationDbContext
{
    public IRepository<AssetType> AssetTypes { get; }
}