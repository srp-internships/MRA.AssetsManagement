using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Web.Shared.AssetSerialHistory;

public class GetAssetSerialHistory
{
    public string? Employee { get; set; }
    public DateOnly Date { get; set; }
    public AssetStatus Status { get; set; }
    public string? Note { get; set; }
}