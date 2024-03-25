using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class Tag : IEntity
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Color { get; set; }
}