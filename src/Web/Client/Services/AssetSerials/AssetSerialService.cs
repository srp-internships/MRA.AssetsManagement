using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerialHistory;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Tags;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetSerials;

public class AssetSerialService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IAssetSerialService
{
    private readonly string _baseAddress = environment.BaseAddress;
    
    public async Task<IEnumerable<GetAssetSerialHistory>> GetAssetSerialHistories(string serial)
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAssetSerialHistory>>($"{_baseAddress}api/assetserials/histories/{serial}");
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }

    public async Task<GetAssetSerial> GetBySerial(string serial)
    {
        var response = await httpClient.GetFromJsonAsync<GetAssetSerial>($"{_baseAddress}api/assetserials/{serial}");
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }

    public async Task<List<GetTag>> SetTagsToAssetSerial(SetTagsToAssetSerialsRequest request)
    {
        var response = await httpClient.PatchAsJsonAsync<List<GetTag>>($"{_baseAddress}api/assetserials/set-tag", request);
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }

    public async Task UpdateSerial(UpdateAssetSerialRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"{_baseAddress}api/assets", request);
        snackbar.ShowIfError(response, "Occured some errors");
    }

    public async Task<IEnumerable<GetAssetSerial>> GetAssetSerials()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAssetSerial>>($"{_baseAddress}api/assetserials/serial");
        snackbar.ShowIfError(response, "Error was occured");
        return response.Result!;
    }
}