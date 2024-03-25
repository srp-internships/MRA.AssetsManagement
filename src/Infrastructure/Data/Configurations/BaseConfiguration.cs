using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public abstract class BaseConfiguration<T> where T : IEntity
{
    protected BaseConfiguration(IMongoDatabase database, string collectionName)
    {
        BsonClassMap.RegisterClassMap<T>(RegisterClassMap);
        Collection = database.GetCollection<T>(collectionName);
        Configure();
    }
    
    public IMongoCollection<T> Collection { get; }

    /// <summary>
    /// see more https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/serialization/class-mapping/
    /// </summary>
    protected virtual void RegisterClassMap(BsonClassMap<T> classMap)
    {
        classMap.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
    }

    protected virtual void Configure() { }
}