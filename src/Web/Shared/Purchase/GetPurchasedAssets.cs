using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Shared.Purchase;

public class GetPurchasedAssets
{
    public required GetAssetType AssetType { get; set; }
    public required string AssetName { get; set; }
    public DateTime DateTime { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}