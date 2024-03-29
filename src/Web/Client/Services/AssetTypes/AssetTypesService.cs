﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Client.Common.Extensions;
using MRA.AssetsManagement.Web.Shared;
using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public class AssetTypesService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IAssetTypesService
    {

        private readonly string _baseAddress = environment.BaseAddress;

        public async Task<GetAssetType> GetAssetTypeById(string id)
        {
            var response = await httpClient.GetFromJsonAsync<GetAssetType>($"{_baseAddress}api/assettypes/{id}");
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }

        public async Task Create(CreateAssetTypeRequest newAssetType)
        {
            var response = await httpClient.PostAsJsonAsync($"{_baseAddress}api/assettypes", newAssetType);
        }

        public async Task Archive(string id)
        {
            var response = await httpClient.PutAsJsonAsync($"{_baseAddress}api/assettypes/archive/{id}", null!);
        }

        public async Task Restore(string id)
        {
            var response = await httpClient.PutAsJsonAsync($"{_baseAddress}api/assettypes/restore/{id}", null!);
        }

        public async Task Update(GetAssetType getAssetType)
        {
            var response = await httpClient.PutAsJsonAsync($"{_baseAddress}api/assettypes", getAssetType);
         }

        public async Task<IEnumerable<Shared.MenuItems.MenuItem>> Fetch()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetAssetType>>($"{_baseAddress}api/assettypes");
            snackbar.ShowIfError(response, "Error was occured.");

            var res = response.Result!.Select(mi => mi.ToMenuItem());
            return res.ToList();
        }
        
    }
}
