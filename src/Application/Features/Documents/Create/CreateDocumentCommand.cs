namespace MRA.AssetsManagement.Application.Features.Documents.Create;

public record CreateDocumentDetailCommand(
    decimal Price,
    int Quantity,
    string? AssetName,
    string AssetTypeId,
    string? AssetId);

public abstract class CreateDocumentCommand
{
    public bool Approved { get; set; } = true;
    public DateTime Date { get; set; }
    public List<CreateDocumentDetailCommand> Details { get; set; } = null!;
}