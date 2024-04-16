namespace MRA.AssetsManagement.Web.Shared.AssetTypes;

public class GetAssetTypeWithAssetsCount
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int AssetsCount { get; set; }
}