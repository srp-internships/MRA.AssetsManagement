using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class Asset : IEntity
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<Properties> Properties { get; set; } = [];
    public string AssetTypeId { get; set; } = null!;

    public override string ToString()
    {
        var properties = string.Join(" ", Properties.Select(x => x.Value));
        return $"{Name} {properties}";
    }
}