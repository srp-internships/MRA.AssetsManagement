using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService : IFetchMenuItemService
    {
        Task<List<GetAssetType>> GetAll();
        Task<GetAssetType> GetAssetTypeBySlug(string slug);
        Task<List<GetAssetTypeWithAssetsCount>> GetAssetTypeWithAssetsCount();
        Task<GetAssetType> Create(CreateAssetTypeRequest newAssetType);
        Task Update(GetAssetType newGetAssetType);
    }
}
