using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.AssetsManagement.Web.Shared.Purchas;

namespace MRA.AssetsManagement.Web.Client.Services.ReportService;

public interface IReportService
{
    Task<List<GetPurchasedAssets>> GetPurchases(PurchasedAssetsRequest request);
    Task<List<GetAssetType>> GetTypes();
    Task<List<GetAssetType>> Fetch2();
}