using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Assets.Queries
{
    public record GetAssetsByTypeIdQuery(string typeId) : IRequest<IReadOnlyCollection<Asset>>;

    public class GetAssetsByIdQueryHandler : IRequestHandler<GetAssetsByTypeIdQuery, IReadOnlyCollection<Asset>>
    {
        private readonly IApplicationDbContext _context;
        public GetAssetsByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Asset>> Handle(GetAssetsByTypeIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Assets.GetAllAsync(x => x.AssetTypeId == request.typeId, cancellationToken);
            return result;
        }
    }

}
