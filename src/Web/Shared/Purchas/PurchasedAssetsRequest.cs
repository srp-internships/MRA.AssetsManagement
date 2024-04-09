namespace MRA.AssetsManagement.Web.Shared.Purchas;

public class PurchasedAssetsRequest
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? AssetId { get; set; }
}