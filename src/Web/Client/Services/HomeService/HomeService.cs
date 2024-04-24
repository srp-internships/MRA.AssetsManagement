using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;
using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.HomeService
{
    public class HomeService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IHomeService
    {
        private readonly string _baseAddress = environment.BaseAddress;

        public async Task<IEnumerable<GetAssetTypeSerial>> Get()
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAssetTypeSerial>>($"{_baseAddress}api/home");
            snackbar.ShowIfError(response, "Error was occured");
            return response.Result!;
        }
    }
}
