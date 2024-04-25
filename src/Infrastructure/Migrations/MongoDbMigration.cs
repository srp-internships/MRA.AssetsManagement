using Microsoft.Extensions.Options;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Infrastructure.Data.Migrations;

namespace MRA.AssetsManagement.Infrastructure.Migrations
{
    public class MongoDbMigration
    {
        private readonly Dictionary<int, IMigration> _migrations;
        private readonly IMongoDatabase _database;
        private readonly IOptions<MongoDbOption> _options;

        public MongoDbMigration(IOptions<MongoDbOption> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            var client = new MongoClient(options.Value.ConnectionString);
            this._database = client.GetDatabase(options.Value.DatabaseName);
            _migrations = new Dictionary<int, IMigration>();
            AddMigration(1, new DefaultVersion(this._database));
            Migrate();
        }

        private void AddMigration(int version, IMigration migration)
        {
            if (!_migrations.TryAdd(version, migration))
            {
                throw new ArgumentException($"Migration for version {version} is already registered.");
            }
        }

        private void Migrate()
        {
            var version = _options.Value.Version;
            if (!_migrations.TryGetValue(version, out IMigration? migration))
            {
                throw new ArgumentException($"Migration for version {version} not found.");
            }

            migration.Up();
            UpdateDatabaseVersion(version);
        }

        private void UpdateDatabaseVersion(int version)
        {
            BsonClassMap.RegisterClassMap<DbVersion>(conf =>
            {
                conf.MapMember(x => x.Version).SetElementName("version");
                conf.MapMember(x => x.DateTime).SetElementName("date");
            });

            var collection = _database.GetCollection<DbVersion>("_versions");
            var filter = Builders<DbVersion>.Filter.Empty;
            var update = Builders<DbVersion>.Update
                .Set(v => v.Version, version)
                .Set(v => v.DateTime, DateTime.Now);
            collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }
}