using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Web.Shared.Assets;

public class GetAssetSerial
{
    public string Id { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Serial { get; set; } = null!;
    public AssetSerialType AssetSerialType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public UserDisplay? Employee { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime From { get; set; }
}

public record AssetSerialType(string Icon, string Name);