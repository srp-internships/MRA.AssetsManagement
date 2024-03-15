
using Microsoft.Extensions.Options;

using MRA.AssetsManagement.Application;
using MRA.AssetsManagement.Infrastructure;
using MRA.AssetsManagement.Infrastructure.Data;
using MRA.AssetsManagement.Infrastructure.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbOption>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services
    .AddApplication()
    .AddInfrastructure();

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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
