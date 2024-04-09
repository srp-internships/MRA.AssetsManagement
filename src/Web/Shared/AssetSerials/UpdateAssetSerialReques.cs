using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Web.Shared.AssetSerials;

public class UpdateAssetSerialRequest
{
    public string Id { get; set; } = null!;
    public UserDisplay? UserDisplay { get; set; }
    public string? Status { get; set; }
}