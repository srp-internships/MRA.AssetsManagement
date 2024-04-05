using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class AssetSerialConfiguration : BaseConfiguration<AssetSerial>
{
    public AssetSerialConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<AssetSerial> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.Serial).SetElementName("serial");
        classMap.MapMember(x => x.Asset).SetElementName("asset");
        classMap.MapMember(x => x.Status).SetElementName("status");
        classMap.MapMember(x => x.EmployeeId).SetElementName("employeeId");
    }
    protected override void Configure()
    {
        var indexKeysDefinition = Builders<AssetSerial>.IndexKeys.Ascending(x => x.Serial);
        Collection.Indexes.CreateOne(new CreateIndexModel<AssetSerial>(indexKeysDefinition, new CreateIndexOptions() { Unique = true }));
    }
}