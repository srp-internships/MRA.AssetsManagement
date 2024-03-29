using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class Asset : IEntity
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string AssetTypeId { get; set; } = null!;
}