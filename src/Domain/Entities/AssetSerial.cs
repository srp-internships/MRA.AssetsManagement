using MRA.AssetsManagement.Domain.Common;
using MRA.AssetsManagement.Domain.Enums;

namespace MRA.AssetsManagement.Domain.Entities;

public record AssetSerial : IAuditableEntity
{
    public string Id { get; set; } = null!;
    public string Serial { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
    public AssetStatus Status { get; set; }
    public UserDisplay? Employee { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime LastModifiedAt { get; set; }
}