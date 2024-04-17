namespace MRA.AssetsManagement.Web.Shared.AssetSerials
{
    public class GetAssetSerials
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Serial { get; set; } = null!;
        public DateOnly From { get; set; }
    }
}
