using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class AssetTypeConfiguration : BaseConfiguration<AssetType>
{
    public AssetTypeConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<AssetType> classMap)
    {
        base.RegisterClassMap(classMap);
        classMap.MapMember(x => x.Name).SetElementName("name");
        classMap.MapMember(x => x.Slug).SetElementName("slug");
        classMap.MapMember(x => x.ShortName).SetElementName("shortName");
        classMap.MapMember(x => x.Properties).SetElementName("properties");
        classMap.MapMember(x => x.Icon).SetElementName("icon");
        classMap.MapMember(x => x.Archived).SetElementName("archived");

        BsonClassMap.RegisterClassMap<Properties>(initializer =>
        {
            initializer.MapMember(x => x.Label).SetElementName("label");
            initializer.MapMember(x => x.Value).SetElementName("value");
        });
    }

    protected override void Configure()
    {
        var nameIndex = Builders<AssetType>.IndexKeys.Ascending(x => x.Name);
        var shortNameIndex = Builders<AssetType>.IndexKeys.Ascending(x => x.ShortName);
        var slugIndex = Builders<AssetType>.IndexKeys.Ascending(x => x.Slug);

        var indexOptions = new CreateIndexOptions { Unique = true };
        var nameIndexModel = new CreateIndexModel<AssetType>(nameIndex, indexOptions);
        var slugIndexModel = new CreateIndexModel<AssetType>(slugIndex, indexOptions);

        var shortNameIndexModel = new CreateIndexModel<AssetType>(shortNameIndex, indexOptions);

        Collection.Indexes.CreateMany(new CreateIndexModel<AssetType>[] { nameIndexModel, shortNameIndexModel, slugIndexModel });
    }
}