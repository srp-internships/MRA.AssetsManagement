using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Client;
using MRA.AssetsManagement.Web.Client.Services.AssetTypes;
using MRA.AssetsManagement.Web.Client.Services.Icons;
using MRA.AssetsManagement.Web.Client.Services.Tags;
using MRA.AssetsManagement.Web.Client.Services.Employees;
using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Client.Services.Assets;
using MRA.AssetsManagement.Web.Client.Services.Report;
using MRA.AssetsManagement.Web.Client.Services.ReportService;
using MRA.AssetsManagement.Web.Client.Services.AssetSerials;
using MRA.BlazorComponents.HttpClient;
using MudBlazor.Services;


using CustomAuthStateProvider = MRA.AssetsManagement.Web.Client.Services.AuthService.CustomAuthStateProvider;
using MRA.AssetsManagement.Web.Client.Services.HomeService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddHttpClientService();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IIconService, IconService>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IAssetTypesService, AssetTypesService>();
builder.Services.AddScoped<ITagsService, TagsService>();
builder.Services.AddScoped<IAssetsService, AssetsService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IAssetSerialService, AssetSerialService>();

await builder.Build().RunAsync();
