using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class DocumentDetailConfiguration : BaseConfiguration<DocumentDetail>
{
    public DocumentDetailConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<DocumentDetail> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.Asset).SetElementName("asset");
        classMap.MapMember(x => x.Quantity).SetElementName("quantity");
        classMap.MapMember(x => x.Price).SetElementName("price");
    }
}