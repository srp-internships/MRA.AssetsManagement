using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Documents.Create;

public class CreateDocumentDetailCommand
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Asset Asset { get; set; } = null!;
}

public abstract class CreateDocumentCommand
{
    public bool Approved { get; set; } = true;
    public DateTime Date { get; set; }
    public List<CreateDocumentDetailCommand> Details { get; set; } = null!;
}