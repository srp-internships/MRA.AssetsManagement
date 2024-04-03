using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class DocumentConfiguration : BaseConfiguration<Document>
{
    public DocumentConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<Document> classMap)
    {
        base.RegisterClassMap(classMap);

        classMap.MapMember(x => x.Approved).SetElementName("approved");
        classMap.MapMember(x => x.Date).SetElementName("date");
        classMap.MapMember(x => x.Details).SetElementName("details");
        
        classMap.SetIsRootClass(true);
        classMap.AddKnownType(typeof(PurchaseDocument));
        
        BsonClassMap.RegisterClassMap<DocumentDetail>(initializer =>
        {
            initializer.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            initializer.MapMember(x => x.Quantity).SetElementName("quantity");
            initializer.MapMember(x => x.Price).SetElementName("price");
            initializer.MapMember(x => x.Asset).SetElementName("asset");
        });
        
        BsonClassMap.RegisterClassMap<PurchaseDocument>(initializer =>
        {
            initializer.MapMember(x => x.Vendor).SetElementName("vendor");
        });
    }
}