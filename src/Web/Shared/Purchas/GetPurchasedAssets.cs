namespace MRA.AssetsManagement.Web.Shared.Purchas;

public class GetPurchasedAssets
{
    public string? AssetType { get; set; }
    public string? AssetName { get; set; }
    public string? AssetSerial { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}