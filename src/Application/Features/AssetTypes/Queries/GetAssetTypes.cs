using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Queries;

public record GetAssetTypesQuery : IRequest<IReadOnlyCollection<Web.Shared.AssetTypes.GetAssetType>>;

public class GetAssetTypesListQuery : IRequestHandler<GetAssetTypesQuery, IReadOnlyCollection<Web.Shared.AssetTypes.GetAssetType>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAssetTypesListQuery(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<Web.Shared.AssetTypes.GetAssetType>> Handle(GetAssetTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.AssetTypes.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyCollection<Web.Shared.AssetTypes.GetAssetType>>(result);
    }
}