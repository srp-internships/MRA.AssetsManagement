using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Data;

public interface IApplicationDbContext
{
    public IRepository<AssetType> AssetTypes { get; }
}