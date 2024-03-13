using Microsoft.Extensions.Options;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Infrastructure.Data.Configurations;

namespace MRA.AssetsManagement.Infrastructure.Data;

public class MongoDbContext : IApplicationDbContext
{
    public MongoDbContext(IOptions<MongoDbOption> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(options.Value.DatabaseName);
        
        var assetConfig = new AssetTypeConfiguration(database);
        var mongoRepository = new MongoRepository<AssetType>(assetConfig.Collection);
        AssetTypes = mongoRepository;
    }

    public IRepository<AssetType> AssetTypes { get; }
}