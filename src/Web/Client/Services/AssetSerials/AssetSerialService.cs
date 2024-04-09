using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Client.Services.Assets;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetSerials;

public class AssetSerialService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment, IAssetsService assetsService) : IAssetSerialService
{
    private readonly string _baseAddress = environment.BaseAddress;

    public event Action? OnChange;

    public async Task<GetAssetSerial> GetBySerial(string serial)
    {
        var response = await httpClient.GetFromJsonAsync<GetAssetSerial>($"{_baseAddress}api/assets/serial/{serial}");
        snackbar.ShowIfError(response, "Occured some errors");
        return response.Result!;
    }

    public async Task UpdateSerial(UpdateAssetSerialRequest request)
    {
        await httpClient.PutAsJsonAsync($"{_baseAddress}api/assets", request);
        await assetsService.GetAssetSerials();
        OnChange?.Invoke();
    }
}