using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public class AssetTypesService(IHttpClientService httpClient, ISnackbar snackbar) : IAssetTypesService
    {
        public event Action? OnChange;
        public IEnumerable<GetAssetType> AssetTypes { get; set; } = Enumerable.Empty<GetAssetType>();
        
        public async Task<IEnumerable<GetAssetType>> GetAssetTypes()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetAssetType>>("http://localhost:5203/api/assettypes");
            snackbar.ShowIfError(response, "Error was occured.");
            AssetTypes = response.Result!;
            return AssetTypes;
        }

        public async Task<GetAssetType> GetAssetTypeById(string id)
        {
            var response = await httpClient.GetFromJsonAsync<GetAssetType>($"http://localhost:5203/api/assettypes/{id}");
            snackbar.ShowIfError(response, "Error was occured.");
            OnChange?.Invoke();
            return response.Result!;
        }

        public async Task Create(CreateAssetTypeRequest newAssetType)
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:5203/api/assettypes", newAssetType);
            await GetAssetTypes();
            OnChange?.Invoke();
        }

        public async Task Archive(string id)
        {
            var response = await httpClient.PutAsJsonAsync($"http://localhost:5203/api/assettypes/archive/{id}", null!);
            await GetAssetTypes();
            OnChange?.Invoke();
        }

        public async Task Restore(string id)
        {
            var response = await httpClient.PutAsJsonAsync($"http://localhost:5203/api/assettypes/restore/{id}", null!);
            await GetAssetTypes();
            OnChange?.Invoke();
        }

        public async Task Update(GetAssetType getAssetType)
        {
            var response = await httpClient.PutAsJsonAsync("http://localhost:5203/api/assettypes", getAssetType);
            await GetAssetTypes();
            OnChange?.Invoke();

        }
    }
}
