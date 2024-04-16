using MediatR;

using MRA.AssetsManagement.Application.Data;

using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Queries;

public class GetAssetTypeWithAssetsCountQuery : IRequest<List<GetAssetTypeWithAssetsCount>>
{

}

public class GetAssetTypeWithAssetsCountQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetAssetTypeWithAssetsCountQuery, List<GetAssetTypeWithAssetsCount>>
{
    public async Task<List<GetAssetTypeWithAssetsCount>> Handle(GetAssetTypeWithAssetsCountQuery request, CancellationToken cancellationToken)
    {
        var assetTypes = await dbContext.AssetTypes.GetAllAsync(cancellationToken);

        var result = new List<GetAssetTypeWithAssetsCount>();
        
        foreach (var assetType in assetTypes)
        {
            result.Add(new GetAssetTypeWithAssetsCount()
            {
                Id = assetType.Id,
                Name = assetType.Name,
                AssetsCount = (await dbContext.AssetSerials.GetAllAsync(x => x.Asset.AssetTypeId == assetType.Id && x.Status == Domain.Enums.AssetStatus.Available, cancellationToken)).Count
            });
        }

        return result.OrderBy(x => x.Name).ToList();
    }
}