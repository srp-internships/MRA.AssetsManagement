using MediatR;
using MRA.AssetsManagement.Application.Data;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public record RestoreAssetTypeCommand(string Id) : IRequest;

public class RestoreAssetTypeCommandHandler : IRequestHandler<RestoreAssetTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public RestoreAssetTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RestoreAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var assetType = await _context.AssetTypes.GetAsync(request.Id);

        if (assetType is null)
            throw new Exception("AssetType with provided Id was not found.");

        assetType.Archived = false;
        await _context.AssetTypes.UpdateAsync(assetType);
    }
}