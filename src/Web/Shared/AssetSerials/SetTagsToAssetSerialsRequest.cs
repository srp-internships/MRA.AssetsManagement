namespace MRA.AssetsManagement.Web.Shared.AssetSerials;

public class SetTagsToAssetSerialsRequest
{
    public string TagId { get; set; } = null!;
    public string AssetSerialId { get; set; } = null!;
    public bool IsAdd { get; set; }
}