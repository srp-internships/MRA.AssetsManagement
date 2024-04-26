namespace MRA.AssetsManagement.Web.Shared.AssetPurchases
{
    public class CreateAssetPurchaseRequest
    {
        public string Vendor { get; set; } = null!;
        public bool Approved { get; set; } = true;
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public List<CreateAssetPurchaseDetail> Details { get; set; } = null!;
    }
}
