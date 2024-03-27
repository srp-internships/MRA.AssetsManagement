using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Data;

public interface IApplicationDbContext
{
    public IRepository<AssetType> AssetTypes { get; }
    public IRepository<Tag> Tags { get; }
    public IRepository<Document> Documents { get; }
    public IRepository<AssetSerial> AssetSerials { get; }
    public IRepository<Asset> Assets { get; }
}