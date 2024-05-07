using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MRA.AssetsManagement.Application;
using MRA.AssetsManagement.Infrastructure;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Infrastructure.Data.Seeder;
using MRA.AssetsManagement.Infrastructure.Migrations;
using MRA.AssetsManagement.Web.Server;
using MRA.AssetsManagement.Web.Server.Filters;

using Serilog;

using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
})
.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example: "{token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    c.CustomSchemaIds(s => s.FullName?.Replace("+", "."));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("JWT:Secret").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        });

builder.Services.AddHttpContextAccessor();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
bool development = app.Environment.IsStaging() || app.Environment.IsDevelopment();
if (development)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var databaseOption = scope.ServiceProvider.GetService<IOptions<MongoDbOption>>();
    if (databaseOption is not null && databaseOption.Value.Seeder)
    {
        var seedService = scope.ServiceProvider.GetService<IDataSeeder>();
        await seedService!.SeedData(development);
    }

    scope.ServiceProvider.GetService<MongoDbMigration>();
}

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();