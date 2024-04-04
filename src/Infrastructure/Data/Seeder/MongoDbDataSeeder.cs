using Microsoft.Extensions.DependencyInjection;
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

    public MongoDbDataSeeder(IApplicationDbContext context, ILogger<MongoDbDataSeeder> logger, IServiceScopeFactory serviceScopeFactory)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
        
        _logger = logger;
        _entitySeeders =
        [
            new AssetTypeEntitySeeder(context.AssetTypes),
            new AssetEntitySeeder(context),
            new AssetSerialEntitySeeder(context),
            new TagEntitySeeder(context.Tags),
            new DocumentEntitySeeder(context),
            new AssetHistoryEntitySeeder(context)
        ];
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