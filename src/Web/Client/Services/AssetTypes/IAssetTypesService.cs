using MRA.AssetsManagement.Web.Shared;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public interface IAssetTypesService
    {
        Task<List<AssetType>> GetAssetTypes();
        Task<AssetType> GetAssetTypeById(string id);

        string GenerateShortName(string name);
    }
}
