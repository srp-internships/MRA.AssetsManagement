using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class PurchaseDocumentConfiguration : BaseConfiguration<PurchaseDocument>
{
    public PurchaseDocumentConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<PurchaseDocument> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.Vendor).SetElementName("vendor");
    }
}