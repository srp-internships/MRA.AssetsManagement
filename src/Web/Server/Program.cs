using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MRA.AssetsManagement.Application;
using MRA.AssetsManagement.Application.Common.Security;
using MRA.AssetsManagement.Infrastructure;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Infrastructure.Data.Seeder;
using MRA.AssetsManagement.Infrastructure.Identity.Services;
using MRA.AssetsManagement.Web.Server;
using MRA.AssetsManagement.Web.Server.Filters;

using Serilog;

using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
}).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Exampel: "{token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>(); 
});

builder.Services.Configure<MongoDbOption>(builder.Configuration.GetSection("MongoDb"));

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
    .AddInfrastructure();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
bool isDevelopment = app.Environment.IsDevelopment();
if (isDevelopment)
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    var databaseOption = scope.ServiceProvider.GetService<IOptions<MongoDbOption>>();
    if (databaseOption is not null && databaseOption.Value.Seeder) {
        var seedService = scope.ServiceProvider.GetService<IDataSeeder>();
        await seedService!.SeedData(isDevelopment);
    }
}

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
