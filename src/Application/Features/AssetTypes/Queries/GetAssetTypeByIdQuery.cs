using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Queries;

public record GetAssetTypeById(string Id) : IRequest<AssetType>;


public class GetAssetTypeByIdQuery : IRequestHandler<GetAssetTypeById, AssetType>
{
    private readonly IApplicationDbContext _context;
    public GetAssetTypeByIdQuery(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AssetType> Handle(GetAssetTypeById request, CancellationToken cancellationToken)
    {
        return await _context.AssetTypes.GetAsync(request.Id);
    }
}
