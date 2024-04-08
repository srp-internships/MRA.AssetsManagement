using MRA.AssetsManagement.Web.Shared.AssetPurchases;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Web.Client.Services.Assets;

public interface IAssetsService
{
    Task<IEnumerable<GetAssetSerial>> GetAssetSerials();
    Task<IEnumerable<GetAsset>> GetAssetsById(string id);
    Task CreatePurchase(CreateAssetPurchaseRequest newAssetPurchase);
}