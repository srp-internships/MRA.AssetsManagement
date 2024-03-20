using Microsoft.Extensions.Logging;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder;

public interface IDataSeeder
{
    Task SeedData(bool isDevelopment);
}   

public class MongoDbDataSeeder : IDataSeeder
{
    private readonly ILogger<MongoDbDataSeeder> _logger;
    private readonly IEntitySeeder[] _entitySeeders;
    public MongoDbDataSeeder(IApplicationDbContext context, ILogger<MongoDbDataSeeder> logger)
    {
        _logger = logger;
        // _entitySeeders = 
        //     [
        //         new AssetTypeEntitySeeder(context.AssetTypes)
        //     ];
    }
    
    public async Task SeedData(bool isDevelopment)
    {
        try
        {
            _logger.LogInformation($"{(isDevelopment ? "Development" : "Production")}: Seeding database ...");
            foreach (var seeder in _entitySeeders)
            {
                if (isDevelopment)
                    await seeder.Development();
                else
                    await seeder.Production();
            }
            _logger.LogInformation("Seeding is done.");
        }
        catch (Exception exp)
        {
            _logger.LogError(exp, $"Cannot seed database!");
        }
    }
}