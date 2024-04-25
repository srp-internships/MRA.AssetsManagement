using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.AssetsManagement.Web.Shared.Enums;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Shared.Assets;

public class GetAssetSerial
{
    public string Id { get; set; } = null!;
    public AssetStatus Status { get; set; }
    public string Serial { get; set; } = null!;
    public List<GetTag> Tags { get; set; } = [];
    public AssetSerialType AssetSerialType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public UserDisplay? Employee { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime From { get; set; }
}

public record AssetSerialType(string Icon, string Name);