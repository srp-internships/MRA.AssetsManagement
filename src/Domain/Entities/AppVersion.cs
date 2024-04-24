using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class AppVersion : IEntity
{
    public string Id { get; set; } = null!;
    public int Version { get; set; }
    public DateTime DateTime { get; set; }
}