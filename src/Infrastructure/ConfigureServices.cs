using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRA.AssetsManagement.Application.Common.Security;
using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Infrastructure.Data.Seeder;
using MRA.AssetsManagement.Infrastructure.Identity;
using MRA.AssetsManagement.Infrastructure.Identity.Services;
using MRA.AssetsManagement.Infrastructure.Migrations;

namespace MRA.AssetsManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<MongoDbMigration>();
        services.AddSingleton<IApplicationDbContext, MongoDbContext>();
        services.AddSingleton<IDataSeeder, MongoDbDataSeeder>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddAuthorization(auth =>
        {
            auth.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            auth.AddPolicy(ApplicationPolicies.Administrator, op => op
                .RequireRole(ApplicationClaimValues.SuperAdministrator, ApplicationClaimValues.Administrator));
        });
        services.AddScoped<ICurrentUserService, CurrentUserService>();


        services.AddHttpClient();

        services.Configure<MongoDbOption>(configuration.GetSection("MongoDb"));
        var corsAllowedHosts = configuration.GetSection("MraAssetsManagement-CORS").Get<string[]>();
        services.AddCors(options =>
        {
            options.AddPolicy("CORS_POLICY", policyConfig =>
            {
                policyConfig.WithOrigins(corsAllowedHosts!)
                    .AllowAnyHeader().AllowCredentials()
                    .AllowAnyMethod();
            });
        });
        return services;
    }
}