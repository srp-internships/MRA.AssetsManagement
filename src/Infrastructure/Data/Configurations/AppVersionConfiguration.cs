using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class AppVersionConfiguration : BaseConfiguration<AppVersion>
{
    public AppVersionConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }
    protected override void RegisterClassMap(BsonClassMap<AppVersion> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.Version).SetElementName("version");
    }

    protected override void Configure()
    {
    }
}