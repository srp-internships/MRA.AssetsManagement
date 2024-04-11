using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;
using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Assets;

public class AssetsService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IAssetsService
{
    private readonly string _baseAddress = environment.BaseAddress;

    public async Task<IEnumerable<GetAssetSerial>> GetAssetSerials()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAssetSerial>>($"{_baseAddress}api/assets/serial");
        snackbar.ShowIfError(response, "Error was occured");
        return response.Result!;
    }
}