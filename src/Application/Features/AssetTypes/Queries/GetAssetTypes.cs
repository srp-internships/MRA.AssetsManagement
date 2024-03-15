using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Queries;

public record GetAssetTypesQuery : IRequest<IReadOnlyCollection<AssetType>>;

public class GetAssetTypesListQuery : IRequestHandler<GetAssetTypesQuery, IReadOnlyCollection<AssetType>>
{
    private readonly IApplicationDbContext _context;
    public GetAssetTypesListQuery(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyCollection<AssetType>> Handle(GetAssetTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.AssetTypes.GetAllAsync(cancellationToken);
    }
}