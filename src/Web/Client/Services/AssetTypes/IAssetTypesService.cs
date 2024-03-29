﻿using MRA.AssetsManagement.Web.Client.Shared.MenuItems;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService : IFetchMenuItemService
    {
        Task<GetAssetType> GetAssetTypeById(string id);
        Task Create(CreateAssetTypeRequest newAssetType);
        Task Update(GetAssetType newGetAssetType);
        Task Archive(string id);
        Task Restore(string id);
    }
}