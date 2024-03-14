using FluentValidation.AspNetCore;

using MRA.AssetsManagement.Infrastructure.Data;

namespace MRA.AssetsManagement.Web.Server;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOption>(configuration.GetSection("MongoDb"));
        
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
}