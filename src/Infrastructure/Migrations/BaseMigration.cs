using MongoDB.Driver;

using MRA.AssetsManagement.Infrastructure.Data.Migrations;

namespace MRA.AssetsManagement.Infrastructure.Migrations;

public abstract class BaseMigration(IMongoDatabase database) : IMigration
{
    protected readonly IMongoDatabase _database = database;

    public virtual void Up() {}
    public virtual void Down() {}
}