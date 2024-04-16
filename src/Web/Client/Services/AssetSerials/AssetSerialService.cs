using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetSerials;

public class AssetSerialService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IAssetSerialService
{
    private readonly string _baseAddress = environment.BaseAddress;

    public async Task<List<GetAssetSerial>> GetAssetSerials(string assetTypeId)
    {
        var response = await httpClient.GetFromJsonAsync<List<GetAssetSerial>>($"{_baseAddress}api/assettypes/serials/{assetTypeId}");
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }


    public async Task<GetAssetSerial> GetBySerial(string serial)
    {
        var response = await httpClient.GetFromJsonAsync<GetAssetSerial>($"{_baseAddress}api/assets/serial/{serial}");
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }

    public async Task UpdateSerial(UpdateAssetSerialRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"{_baseAddress}api/assets", request);
        snackbar.ShowIfError(response, "Occured some errors");
    }
}