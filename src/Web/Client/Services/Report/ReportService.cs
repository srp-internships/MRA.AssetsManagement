using System.Globalization;
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
        var fromDateStr = request.FromDate?.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
        var toDateStr = request.ToDate?.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);

        var url = $"{_baseUrl}api/Report?FromDate={fromDateStr}&ToDate={toDateStr}";
        var response = await _httpClient.GetFromJsonAsync<List<GetPurchasedAssets>>(url);
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
        var response = await _httpClient.GetFromJsonAsync<List<GetAssetType>>($"{_baseUrl}api/AssetTypes");
        _snackbar.ShowIfError(response, "Error was occured.");
        return response.Result!;
    }
}