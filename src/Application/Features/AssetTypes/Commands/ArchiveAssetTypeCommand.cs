using MediatR;

using MRA.AssetsManagement.Application.Data;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public record ArchiveAssetTypeCommand(string Id) : IRequest;

public class ArchiveAssetTypeCommandHandler : IRequestHandler<ArchiveAssetTypeCommand>
{
    public ArchiveAssetTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    private readonly IApplicationDbContext _context;

    public async Task Handle(ArchiveAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var assetType = await _context.AssetTypes.GetAsync(request.Id);

        if (assetType is null)
            throw new Exception("AssetType with provided Id was not found.");

        assetType.Archived = true;

        await _context.AssetTypes.UpdateAsync(assetType);
    }
}