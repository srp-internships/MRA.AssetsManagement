using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class AppVersion : IEntity
{
    public string Id { get; set; }
    public int Version { get; set; }
}