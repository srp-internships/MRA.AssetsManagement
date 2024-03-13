using Microsoft.Extensions.DependencyInjection;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Infrastructure.Data;

namespace MRA.AssetsManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    { 
        services.AddSingleton<IApplicationDbContext, MongoDbContext>();
        return services;
    }
}