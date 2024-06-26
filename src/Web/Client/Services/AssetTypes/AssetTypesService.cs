﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MRA.AssetsManagement.Web.Client.Common.Extensions;
using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public class AssetTypesService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IAssetTypesService
    {

        private readonly string _baseAddress =  $"{environment.BaseAddress}api/";

        public async Task<GetAssetType> GetAssetTypeBySlug(string slug)
        {
            var response = await httpClient.GetFromJsonAsync<GetAssetType>($"{_baseAddress}assettypes/{slug}");
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }

        public async Task<GetAssetType> Create(CreateAssetTypeRequest newAssetType)
        {
            var response = await httpClient.PostAsJsonAsync<GetAssetType>($"{_baseAddress}assettypes", newAssetType);
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }
        
        public async Task<bool> Update(GetAssetType getAssetType)
        {
            var response = await httpClient.PutAsJsonAsync<bool>($"{_baseAddress}assettypes", getAssetType);
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }

        public async Task<List<MenuItem>> Fetch()
        {
            var result = await GetAll();
            return result.Select(mi => mi.ToMenuItem()).ToList();
        }

        public async Task<List<GetAssetType>> GetAll()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetAssetType>>($"{_baseAddress}assettypes");
            snackbar.ShowIfError(response, "Error was occured.");

            return response.Result!;
        }

        public async Task<List<GetAssetTypeSerial>> GetAssetTypeSerials()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetAssetTypeSerial>>($"{_baseAddress}assettypes/serials");
            snackbar.ShowIfError(response, "Error was occured.");
            return response.Result!;
        }
    }
}
