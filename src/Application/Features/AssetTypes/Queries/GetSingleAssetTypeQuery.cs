using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Queries;

public record GetSingleAssetTypeQuery(string Slug) : IRequest<GetAssetType>;



public class GetSingleAssetTypeQueryHandler : IRequestHandler<GetSingleAssetTypeQuery, GetAssetType>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetSingleAssetTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<GetAssetType>> Handle(GetAssetTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.AssetTypes.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyCollection<GetAssetType>>(result);
    }

    public async Task<GetAssetType> Handle(GetSingleAssetTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.AssetTypes.GetAsync(x => x.Slug == request.Slug, cancellationToken);
        return _mapper.Map<GetAssetType>(result);
    }
}