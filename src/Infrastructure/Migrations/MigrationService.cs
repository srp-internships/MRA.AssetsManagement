using MongoDB.Driver;
using MRA.AssetsManagement.Domain.Entities;

using Tag = MRA.AssetsManagement.Domain.Entities.Tag;

namespace MRA.AssetsManagement.Infrastructure.Migrations;

public class MyMigration2 : IMigration
{
    private const string Version = "2";
    private readonly IMongoCollection<AppVersion> _appVersionCollection;
    private readonly IMongoCollection<Tag> _tagCollection;
    
    public MyMigration2()
    {
        var client = new MongoClient();
        IMongoDatabase database = client.GetDatabase("mra-assets");
        _appVersionCollection = database.GetCollection<AppVersion>("version");
        _tagCollection = database.GetCollection<Tag>("tags");
    }
    public void Up()
    {
        var filter = Builders<Tag>.Filter.Empty;
        var update = Builders<Tag>.Update.Set("Cod", "");
        var updateOptions = new UpdateOptions { IsUpsert = false };
        _tagCollection.UpdateMany(filter, update, updateOptions);
        SaveVersionToDatabase(Version);
    }
    public void Down()
    {
        throw new NotImplementedException();
    }
    private void SaveVersionToDatabase(string version)
    {
        var filter = Builders<AppVersion>.Filter.Empty;
        var update = Builders<AppVersion>.Update.Set(v => v.Version, version);
        _appVersionCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
    }
}

public class MyMigration3 : BaseMigration
{
    private readonly IMongoCollection<Tag> _tagCollection;
    public MyMigration3() : base("3")
    {
        _tagCollection = _database.GetCollection<Tag>("tags");
    }

    public override void Up()
    {
        var filter = Builders<Tag>.Filter.Empty;
        var update = Builders<Tag>.Update.Unset("Cod"); // Удаляем значение по умолчанию
        var updateOptions = new UpdateOptions { IsUpsert = false };
        _tagCollection.UpdateMany(filter, update, updateOptions);
        SaveVersionToDatabase();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
