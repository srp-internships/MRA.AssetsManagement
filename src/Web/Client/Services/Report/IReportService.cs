using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.AssetsManagement.Web.Shared.Purchase;

namespace MRA.AssetsManagement.Web.Client.Services.ReportService;

public interface IReportService
{
    Task<List<GetPurchasedAssets>> GetPurchases(PurchasedAssetsRequest request);
}