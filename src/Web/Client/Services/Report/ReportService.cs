using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Client.Services.ReportService;
using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.AssetsManagement.Web.Shared.Purchas;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;
using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Report;

public class ReportService : IReportService
{
    private readonly IHttpClientService _httpClient;
    private readonly ISnackbar _snackbar;
    private readonly string _baseUrl;
    
    public ReportService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _baseUrl = webAssemblyHostEnvironment.BaseAddress;
    }
    public async Task<List<GetPurchasedAssets>> GetPurchases(PurchasedAssetsRequest request)
    {
        var response = await _httpClient.GetFromJsonAsync<List<GetPurchasedAssets>>($"{_baseUrl}api/Report", request);
        _snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }

    public async Task<List<GetAssetType>> GetTypes()
    {
        var response = await _httpClient.GetFromJsonAsync<List<GetAssetType>>($"{_baseUrl}api/AssetTypes");
        _snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }
    
    public async Task<List<GetAssetType>> Fetch2()
    {
        var response = await _httpClient.GetFromJsonAsync<List<GetAssetType>>($"{_baseUrl}api/assettypes");
        _snackbar.ShowIfError(response, "Error was occured.");
        return response.Result!;
    }
}