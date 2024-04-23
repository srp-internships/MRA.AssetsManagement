using MongoDB.Driver;

namespace MRA.AssetsManagement.Infrastructure.Migrations;
public class DefaultVersion(IMongoClient client) : BaseMigration(client)
{
    public override void Up()
    {
    }
    public override void Down()
    {
    }
}