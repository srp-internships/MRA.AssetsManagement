using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class AssetHistoryConfiguration : BaseConfiguration<AssetHistory>
{
    public AssetHistoryConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<AssetHistory> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.UserId).SetElementName("userId");
        classMap.MapMember(x => x.HistoryAssetSerial).SetElementName("serial");
        classMap.MapMember(x => x.DateTime).SetElementName("dateTime");
        classMap.MapMember(x => x.Note).SetElementName("note");

        BsonClassMap.RegisterClassMap<HistoryAssetSerial>(initializer =>
        {
            initializer.MapMember(x => x.Serial).SetElementName("serial");
            initializer.MapMember(x => x.Asset).SetElementName("asset");
            initializer.MapMember(x => x.Status).SetElementName("status");
            initializer.MapMember(x => x.Employee).SetElementName("employee");
        });
    }
    
    protected override void Configure()
    {
        var indexKeysDefinition = Builders<AssetHistory>.IndexKeys.Descending(x => x.DateTime);
        Collection.Indexes.CreateOne(new CreateIndexModel<AssetHistory>(indexKeysDefinition));
    }
}