using MRA.AssetsManagement.Web.Client.Shared.MenuItems;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService : IFetchService
    {
        Task<GetAssetType> GetAssetTypeById(string id);
        Task Create(CreateAssetTypeRequest newAssetType);
        Task Update(GetAssetType newGetAssetType);
        Task Archive(string id);
        Task Restore(string id);

        IEnumerable<GetAssetType> AssetTypes { get; set; }
        event Action OnChange;
    }
}
