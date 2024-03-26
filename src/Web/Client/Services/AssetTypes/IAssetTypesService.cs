using MRA.AssetsManagement.Web.Shared;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService
    {
        Task GetAssetTypes();
        Task<AssetType> GetAssetTypeById(string id);
        Task Create(CreateAssetTypeDto newAssetType);
        Task Update(AssetType newAssetType);
        Task Archive(string id);
        Task Restore(string id);

        string GenerateShortName(string name);
        IEnumerable<AssetType> AssetTypes { get; set; }
        event Action OnChange;
    }
}
