using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Application.Features.AssetTypes.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Assets.Queries
{
    public record GetAssetsQuery : IRequest<IReadOnlyCollection<Asset>>;

    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, IReadOnlyCollection<Asset>>
    {
        private readonly IApplicationDbContext _context;

        public GetAssetsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Asset>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Assets.GetAllAsync();
            return result;
        }
    }
}
