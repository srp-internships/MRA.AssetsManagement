using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Client.Common.Extensions;
using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.Tags;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Tags
{
    public class TagsService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : ITagsService
    {
        private readonly string _baseAddress =  $"{environment.BaseAddress}api/";

        public async Task<GetTag> GetTagById(string id)
        {
            var response = await httpClient.GetFromJsonAsync<GetTag>($"{_baseAddress}tags/{id}");
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }

        public async Task<GetTag> Create(CreateTagRequest newTag)
        {
            var response = await httpClient.PostAsJsonAsync<GetTag>($"{_baseAddress}tags", newTag);
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }

        public async Task Delete(string id)
        {
            var response = await httpClient.DeleteAsync($"{_baseAddress}tags/{id}");
            snackbar.ShowIfError(response, "Error was occured.");
        }

        public async Task<bool> Update(GetTag newTag)
        {
            var response = await httpClient.PutAsJsonAsync<bool>($"{_baseAddress}tags", newTag);
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }
        
        public async Task<List<MenuItem>> Fetch()
        {
            var result = await GetAll();
            return result.Select(mi => mi.ToMenuItem()).ToList();
        }

        public async Task<List<GetTag>> GetAll()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetTag>>($"{_baseAddress}tags");
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }
    }
}
