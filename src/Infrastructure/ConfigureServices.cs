using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Web.Client;
using MRA.AssetsManagement.Infrastructure.Data.Seeder;

namespace MRA.AssetsManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    { 
        services.AddSingleton<IApplicationDbContext, MongoDbContext>();
        services.AddSingleton<IDataSeeder, MongoDbDataSeeder>();
        return services;
    }
}