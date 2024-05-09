using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Client.Services.ReportService;
using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.AssetsManagement.Web.Shared.Purchase;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;
using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Report;

public class ReportService(
    IHttpClientService httpClient,
    ISnackbar snackbar,
    IWebAssemblyHostEnvironment environment)
    : IReportService
{
    private readonly string _baseUrl =  $"{environment.BaseAddress}api/";

    public async Task<List<GetPurchasedAssets>> GetPurchases(PurchasedAssetsRequest request)
    {
        var fromDateStr = request.FromDate?.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
        var toDateStr = request.ToDate?.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);

        var url = $"{_baseUrl}Report?FromDate={fromDateStr}&ToDate={toDateStr}&AssetTypeId={request.AssetTypeId}";
        var response = await httpClient.GetFromJsonAsync<List<GetPurchasedAssets>>(url);
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }
}