using MongoDB.Driver;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Migrations;

public abstract class BaseMigration : IMigration
{
    private readonly IMongoCollection<AppVersion> _appVersionCollection;
    private readonly string _version;
    protected readonly IMongoDatabase _database;

    protected BaseMigration(string version)
    {
        _version = version;
        var client = new MongoClient();
        _database = client.GetDatabase("mra-assets");
        _appVersionCollection = _database.GetCollection<AppVersion>("version");
    }

    public abstract void Up();
    public abstract void Down();

    protected void SaveVersionToDatabase()
    {
        var filter = Builders<AppVersion>.Filter.Empty;
        var update = Builders<AppVersion>.Update.Set(v => v.Version, _version);
        _appVersionCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
    }
}