using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService : IFetchMenuItemService
    {
        Task<GetAssetType> GetAssetTypeBySlug(string slug);
        Task<List<GetAssetTypeWithAssetsCount>> GetAssetTypeWithAssetsCount();
        Task<GetAssetType> Create(CreateAssetTypeRequest newAssetType);
        Task Update(GetAssetType newGetAssetType);
    }
}
