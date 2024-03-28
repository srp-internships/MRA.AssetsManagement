using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class AssetHistory : IEntity
{
    public string Id { get; set; } = null!;
    public AssetSerial AssetSerial { get; set; } = null!;
    public string? UserId { get; set; }
    public string? Employee { get; set; }
    public DateTime DateTime { get; set; }
}