using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using Tag = MRA.AssetsManagement.Domain.Entities.Tag;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class TagConfiguration : BaseConfiguration<Tag>
{
    public TagConfiguration(IMongoDatabase database) : base(database, "tags")
    {
    }

    protected override void RegisterClassMap(BsonClassMap<Tag> classMap)
    {
        base.RegisterClassMap(classMap);
        classMap.MapMember(x => x.Name).SetElementName("name");
        classMap.MapMember(x => x.Color).SetElementName("color");
    }

    protected override void Configure() { }
}