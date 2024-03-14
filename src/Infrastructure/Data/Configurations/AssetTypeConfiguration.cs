using MongoDB.Bson.Serialization;
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
        classMap.MapMember(x => x.ShortName).SetElementName("shortName");
        classMap.MapMember(x => x.Icon).SetElementName("icon");
        classMap.MapMember(x => x.Archived).SetElementName("archived");
    }

    protected override void CreateIndexIfRequired()
    {
        var nameIndex = Builders<AssetType>.IndexKeys.Ascending(x => x.Name);
        var shortNameIndex = Builders<AssetType>.IndexKeys.Ascending(x => x.ShortName);

        var indexOptions = new CreateIndexOptions { Unique = true };
        var nameIndexModel = new CreateIndexModel<AssetType>(nameIndex, indexOptions);

        var shortNameIndexModel = new CreateIndexModel<AssetType>(shortNameIndex, indexOptions);

        Collection.Indexes.CreateMany(new CreateIndexModel<AssetType>[] { nameIndexModel, shortNameIndexModel });
    }
}