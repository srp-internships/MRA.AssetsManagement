using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Shared.AssetPurchases;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Assets;

public class AssetsService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IAssetsService
{
    private readonly string _baseAddress =  $"{environment.BaseAddress}api/";
    public async Task<IEnumerable<GetAssetSerial>> GetAssetSerials()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAssetSerial>>($"{_baseAddress}assets/serial");
        snackbar.ShowIfError(response, "Error was occured");
        return response.Result!;
    }

    public async Task<PagedList<GetAssetSerial>> GetPagedAssetSerials(AssetsFilterOptions assetsFilterOptions)
    {
        var response = await httpClient.GetFromJsonAsync<PagedList<GetAssetSerial>>($"{_baseAddress}assets/page", assetsFilterOptions);
        snackbar.ShowIfError(response, "Error was occured.");
        return response.Result!;
    }

    public async Task<IEnumerable<GetAsset>> GetAssetsByTypeId(string typeId)
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAsset>>($"{_baseAddress}assets/{typeId}");
        snackbar.ShowIfError(response, "Error was occured.");
        return response.Result!;
    }

    public async Task CreatePurchase(CreateAssetPurchaseRequest newAssetPurchase)
    {
        var response = await httpClient.PostAsJsonAsync($"{_baseAddress}assets/purchase", newAssetPurchase);
        snackbar.ShowIfError(response, "Error was occured.");
    }

    public async Task<GetAsset> CreateAsset(CreateAssetRequest newAsset)
    {
        var response = await httpClient.PostAsJsonAsync<GetAsset>($"{_baseAddress}assets", newAsset);
        snackbar.ShowIfError(response, "Error was occured.");
        return response.Result!;
    }
}