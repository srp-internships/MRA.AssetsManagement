using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using MRA.AssetsManagement.Application.Common.Behaviors;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;


        services.AddValidatorsFromAssemblyContaining<CreateAssetTypeRequest>();
        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(assembly);
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            
            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        return services;
    }
}