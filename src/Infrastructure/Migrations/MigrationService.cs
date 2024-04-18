using MongoDB.Bson;
using MongoDB.Driver;

using MRA.AssetsManagement.Application.Data;

namespace MRA.AssetsManagement.Infrastructure.Migrations;

public class MigrationService
{
    private readonly IMongoDatabase _database;

    public MigrationService(string connectionString,string dataBaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(dataBaseName);
    }
    public void ApplyMigrations()
    {
        var collection = _database.GetCollection<BsonDocument>("migrations");
        var latestMigration = GetLatestMigration(collection);

        // Проверяем, были ли уже применены все миграции
        if (latestMigration == null)
        {
            ApplyInitialMigration(collection);
            return;
        }

        // Применяем миграции, начиная с последней примененной
        if (latestMigration.Version < 1)
        {
            ApplyMigration1(collection);
        }
        // Добавьте другие миграции по мере необходимости
    }

    private BsonDocument? GetLatestMigration(IMongoCollection<BsonDocument> collection)
    {
        return collection.Find(Builders<BsonDocument>.Filter.Empty)
            .SortByDescending(m => m["version"])
            .FirstOrDefault();
    }

    private void ApplyInitialMigration(IMongoCollection<BsonDocument> collection)
    {
        // Применяем начальную миграцию
        var migration = new BsonDocument
        {
            { "version", 1 },
            { "description", "Remove Date field from Document entity" },
            { "date", DateTime.UtcNow },
            { "changes", new BsonDocument
                {
                    { "$unset", new BsonDocument("Date", 1) }
                }
            }
        };
        collection.InsertOne(migration);
    }

    private void ApplyMigration1(IMongoCollection<BsonDocument> collection)
    {
        // Применяем миграцию версии 1
        // Добавьте здесь операции миграции для версии 1
    }

    // Добавьте другие методы ApplyMigrationX() для других миграций по мере необходимости
}