namespace MRA.AssetsManagement.Web.Shared.AssetSerials;

public class GetAssetTypeSerial
{
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public List<GetAssetSerials> AssetSerials { get; set; } = [];
}