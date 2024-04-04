namespace MRA.AssetsManagement.Web.Shared.Assets;

public class GetAssetSerial
{
    public string Status { get; set; } = null!;
    public string Serial { get; set; } = null!;
    public AssetSerialType AssetSerialType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? AssignedTo { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime From { get; set; }
}

public record AssetSerialType(string Icon, string Name);