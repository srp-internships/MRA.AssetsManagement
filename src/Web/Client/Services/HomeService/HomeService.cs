using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;
using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.HomeService
{
    public class HomeService(IHttpClientService httpClient, ISnackbar snackbar, IConfiguration configuration) : IHomeService
    {
        private readonly string _baseAddress = configuration["AssetsManagementApiBaseAddress"]!;

        public async Task<IEnumerable<GetAssetTypeSerial>> Get()
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<GetAssetTypeSerial>>($"{_baseAddress}home");
            snackbar.ShowIfError(response, "Error was occured");
            return response.Result!;
        }
    }
}
