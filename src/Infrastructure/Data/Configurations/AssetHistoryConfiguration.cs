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
        classMap.MapMember(x => x.AssetSerial).SetElementName("asset");
        classMap.MapMember(x => x.Employee).SetElementName("employee");
        classMap.MapMember(x => x.DateTime).SetElementName("dateTime");
    }
}