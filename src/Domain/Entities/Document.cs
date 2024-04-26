using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities;

public class Document : IEntity
{
    public string Id { get; set; } = null!;
    public bool Approved { get; set; } = true;
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public List<DocumentDetail> Details { get; set; } = null!;
}