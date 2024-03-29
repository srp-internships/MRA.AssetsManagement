using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Shared;
using MRA.AssetsManagement.Web.Shared.Tags;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Tags
{
    public class TagsService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : ITagsService
    {
        public IEnumerable<MenuItem> MenuItems { get; set; } = Enumerable.Empty<MenuItem>();
        public IEnumerable<GetTags> Tags { get; set; } = Enumerable.Empty<GetTags>();

        private readonly string _baseAddress = environment.BaseAddress;

        public async Task Create(CreateTagRequest newTag)
        {
            var response = await httpClient.PostAsJsonAsync($"{_baseAddress}api/tags", newTag);
            snackbar.ShowIfError(response, "Error was occured.");
            await GetMenuItems();
        }

        public async Task Delete(string id)
        {
            var response = await httpClient.DeleteAsync($"{_baseAddress}api/tags/{id}");
            snackbar.ShowIfError(response, "Error was occured.");
            await GetMenuItems();
        }

        public async Task Update(GetTags newTag)
        {
            var response = await httpClient.PutAsJsonAsync($"{_baseAddress}api/tags", newTag);
            snackbar.ShowIfError(response, "Error was occured.");
            await GetMenuItems();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetTags>>($"{_baseAddress}api/tags");
            snackbar.ShowIfError(response, "Error was occured.");

            Tags = response.Result!;
            MenuItems = response.Result!.Select(mi => new MenuItem
            {
                Route = $"/settings/tags/{mi.Id}",
                Name = mi.Name
            }).ToList();

            return MenuItems;
        }
    }
}
