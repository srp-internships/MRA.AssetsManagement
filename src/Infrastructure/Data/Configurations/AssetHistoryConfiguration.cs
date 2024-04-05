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
        classMap.MapMember(x => x.AssetSerial).SetElementName("serial");
        classMap.MapMember(x => x.DateTime).SetElementName("dateTime");
    }
    
    protected override void Configure()
    {
        var indexKeysDefinition = Builders<AssetHistory>.IndexKeys.Descending(x => x.DateTime);
        Collection.Indexes.CreateOne(new CreateIndexModel<AssetHistory>(indexKeysDefinition));
    }
}