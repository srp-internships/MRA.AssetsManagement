using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Common;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Infrastructure.Data.Configurations;


using Tag = MRA.AssetsManagement.Domain.Entities.Tag;

namespace MRA.AssetsManagement.Infrastructure.Data;

public class MongoDbContext : IApplicationDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbOption> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);

        AssetTypes = GetRepository<AssetTypeConfiguration, AssetType>("asset-types");
        Tags = GetRepository<TagConfiguration, Tag>("tags");
        Documents = GetRepository<DocumentConfiguration, Document>("documents");
        AssetSerials = GetRepository<AssetSerialConfiguration, AssetSerial>("asset-serials");
        Assets = GetRepository<AssetConfiguration, Asset>("assets");
    }

    public IRepository<AssetType> AssetTypes { get; }
    public IRepository<Tag> Tags { get; }
    public IRepository<Document> Documents { get; }
    public IRepository<AssetSerial> AssetSerials { get; }
    public IRepository<Asset> Assets { get; }
    

    private MongoRepository<TEntity> GetRepository<TConfiguration, TEntity>(string collectionName) where TConfiguration : BaseConfiguration<TEntity>
                                                                            where TEntity : IEntity
    {
        var config = (TConfiguration) Activator.CreateInstance(typeof(TConfiguration), args: [_database, collectionName])!;
        return new MongoRepository<TEntity>(config.Collection);
    }
}