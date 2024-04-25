using MongoDB.Driver;

namespace MRA.AssetsManagement.Infrastructure.Migrations;
public class DefaultVersion(IMongoDatabase database) : BaseMigration(database)
{
}