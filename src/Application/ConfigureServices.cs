using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MRA.AssetsManagement.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}