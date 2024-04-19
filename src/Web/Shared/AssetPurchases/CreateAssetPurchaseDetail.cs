using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Shared.AssetPurchases
{
    public class CreateAssetPurchaseDetail
    {
        public GetAssetType AssetType { get; set; } = null!;
        public GetAsset Asset { get; set; } = null!;
        public string Serials { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
    }
}
