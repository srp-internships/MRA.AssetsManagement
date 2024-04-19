using MRA.AssetsManagement.Web.Shared.AssetPurchases;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Web.Client.Services.Assets;

public interface IAssetsService
{
    Task<IEnumerable<GetAsset>> GetAssetsByTypeId(string typeId);
    Task CreatePurchase(CreateAssetPurchaseRequest newAssetPurchase);
    Task<GetAsset> CreateAsset(CreateAssetRequest newAsset);
}