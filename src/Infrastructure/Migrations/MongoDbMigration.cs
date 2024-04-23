using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Infrastructure.Data;

namespace MRA.AssetsManagement.Infrastructure.Migrations
{
    public class MongoDbMigration
    {
        private readonly Dictionary<int, IMigration> _migrations;
        private readonly IMongoClient _client;
        private readonly IOptions<MongoDbOption> _options;

        public MongoDbMigration(IOptions<MongoDbOption> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _client = new MongoClient(options.Value.ConnectionString);
            _migrations = new Dictionary<int, IMigration>();
            AddMigration(1, new DefaultVersion(_client));
        }

        private void AddMigration(int version, IMigration migration)
        {
            if (!_migrations.TryAdd(version, migration))
            {
                throw new ArgumentException($"Migration for version {version} is already registered.");
            }
        }

        public void Migrate(int version)
        {
            if (_migrations.TryGetValue(version, out IMigration? migration))
            {
                migration.Up();
                SaveVersionToDatabase(version);
            }
            else
            {
                throw new ArgumentException($"Migration for version {version} not found.");
            }
        }

        private void SaveVersionToDatabase(int version)
        {
            var db = _client.GetDatabase(_options.Value.DatabaseName);
            var collection = db.GetCollection<AppVersion>("version");
            var filter = Builders<AppVersion>.Filter.Empty;
            var update = Builders<AppVersion>.Update.Set(v => v.Version, version);
            collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }
}