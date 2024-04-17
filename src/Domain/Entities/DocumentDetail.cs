using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class DocumentDetail : IEntity
{
    public string Id { get; set; } = null!;
    public Asset Asset { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}