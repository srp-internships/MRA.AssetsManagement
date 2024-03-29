using MRA.AssetsManagement.Web.Shared;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService
    {
        Task<IEnumerable<MenuItem>> GetMenuItems();
        Task<GetAssetType> GetAssetTypeById(string id);
        Task Create(CreateAssetTypeRequest newAssetType);
        Task Update(GetAssetType newGetAssetType);
        Task Archive(string id);
        Task Restore(string id);

        IEnumerable<MenuItem> MenuItems { get; set; }
        IEnumerable<GetAssetType> AssetTypes { get; set; }

    }
}
