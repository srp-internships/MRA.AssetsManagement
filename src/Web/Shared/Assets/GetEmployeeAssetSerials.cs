namespace MRA.AssetsManagement.Web.Shared.Assets;

public class GetEmployeeAssetSerials
{
    public string AssetType { get; set; } = null!;
    public string AssetName { get; set; } = null!;
    public string Serial { get; set; } = null!;
    public DateOnly From { get; set; }
}