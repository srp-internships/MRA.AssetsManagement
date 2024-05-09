namespace MRA.AssetsManagement.Web.Shared.Purchase;

public class PurchasedAssetsRequest
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? AssetTypeId { get; set; }
}