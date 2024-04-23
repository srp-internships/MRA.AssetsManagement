using MongoDB.Driver;

namespace MRA.AssetsManagement.Infrastructure.Migrations;

public abstract class BaseMigration(IMongoClient client) : IMigration
{
    protected readonly IMongoDatabase _database = client.GetDatabase("mra-assets");

    public abstract void Up();
    public abstract void Down();
}