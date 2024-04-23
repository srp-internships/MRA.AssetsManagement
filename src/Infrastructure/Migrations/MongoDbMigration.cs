namespace MRA.AssetsManagement.Infrastructure.Migrations;

public class MongoDbMigration
{
    private readonly Dictionary<int, IMigration> _migrations;

    public MongoDbMigration(IEnumerable<IMigration> migrations)
    {
        _migrations = new Dictionary<int, IMigration>();
        AddMigration(2,new MyMigration2());
        AddMigration(3,new MyMigration3());
    }
    private void AddMigration(int version, IMigration migration)
    {
        _migrations.Add(version, migration);
    }

    public void Migrate(int version)
    {
        if (_migrations.ContainsKey(version))
        {
            _migrations[version].Up();
        }
        else
        {
            throw new ArgumentException($"Migration for version {version} not found.");
        }
    }
}
