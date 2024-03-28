namespace MRA.AssetsManagement.Web.Shared.AssetTypes
{
    public class GetAssetType
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public bool Archived { get; set; }
    }
}
