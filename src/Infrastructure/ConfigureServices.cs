using Microsoft.Extensions.DependencyInjection;

using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Infrastructure.Data.Seeder;
using MRA.AssetsManagement.Infrastructure.Identity.Services;
using MRA.AssetsManagement.Infrastructure.Migrations;

namespace MRA.AssetsManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    { 
        services.AddSingleton<IApplicationDbContext, MongoDbContext>();
        services.AddSingleton<IDataSeeder, MongoDbDataSeeder>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddSingleton<MongoDbMigration>();
        
        return services;
    }
}