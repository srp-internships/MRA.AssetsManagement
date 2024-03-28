using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService
    {
        Task<IEnumerable<GetAssetType>> GetAssetTypes();
        Task<GetAssetType> GetAssetTypeById(string id);
        Task Create(CreateAssetTypeRequest newAssetType);
        Task Update(GetAssetType newGetAssetType);
        Task Archive(string id);
        Task Restore(string id);

        IEnumerable<GetAssetType> AssetTypes { get; set; }
        event Action OnChange;
    }
}
