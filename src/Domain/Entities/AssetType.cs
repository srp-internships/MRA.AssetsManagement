using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class AssetType : IEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
}