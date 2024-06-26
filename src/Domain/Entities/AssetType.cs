using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class AssetType : IEntity
{
    public string Id { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public List<Properties> Properties { get; set; } = [];
    public string Icon { get; set; } = null!;
    public bool Archived { get; set; }
}