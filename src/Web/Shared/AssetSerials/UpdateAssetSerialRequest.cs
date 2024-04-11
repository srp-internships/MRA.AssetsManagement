using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Web.Shared.AssetSerials;

public class UpdateAssetSerialRequest
{
    public string Id { get; set; } = null!;
    public UserDisplay? UserDisplay { get; set; }
    public AssetStatus Status { get; set; }
}