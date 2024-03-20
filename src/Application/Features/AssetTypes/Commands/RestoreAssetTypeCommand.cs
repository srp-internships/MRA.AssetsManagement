using MediatR;

using MRA.AssetsManagement.Application.Common.Exceptions;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

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
        var assetType = await _context.AssetTypes.GetAsync(request.Id, cancellationToken);

        if (assetType is null)
            throw new NotFoundEntityException(nameof(AssetType), request.Id);

        assetType.Archived = false;
        await _context.AssetTypes.UpdateAsync(assetType, cancellationToken);
    }
}