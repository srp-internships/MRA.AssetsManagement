using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class AssetTypeConfiguration : BaseConfiguration<AssetType>
{
    public AssetTypeConfiguration(IMongoDatabase database) : base(database, "asset-types")
    {
    }

    protected override void RegisterClassMap(BsonClassMap<AssetType> classMap)
    {
        base.RegisterClassMap(classMap);
        classMap.MapMember(x => x.Name).SetElementName("name");
        
    }
}