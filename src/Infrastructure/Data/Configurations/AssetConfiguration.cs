using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class AssetConfiguration : BaseConfiguration<Asset>
{
    public AssetConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<Asset> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.Name).SetElementName("name");
        classMap.MapMember(x => x.AssetTypeId).SetElementName("assetTypeId");
        classMap.UnmapMember(x=> x.AssetType);
    }
}