using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Infrastructure.Data.Configurations;

using Tag = MRA.AssetsManagement.Domain.Entities.Tag;

namespace MRA.AssetsManagement.Infrastructure.Data;

public class MongoDbContext : IApplicationDbContext
{
    public MongoDbContext(IOptions<MongoDbOption> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(options.Value.DatabaseName);
        
        var assetConfig = new AssetTypeConfiguration(database);
        var tagConfig = new TagConfiguration(database);
        var mongoRepository = new MongoRepository<AssetType>(assetConfig.Collection);
        AssetTypes = mongoRepository;
        Tags = new MongoRepository<Tag>(tagConfig.Collection);
    }

    public IRepository<AssetType> AssetTypes { get; }
    public IRepository<Tag> Tags { get; }
}