using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Queries;

public class GetAssetTypeWithAssetsCountQuery : IRequest<List<GetAssetTypeSerial>>
{

}

public class GetAssetTypeWithAssetsCountQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetAssetTypeWithAssetsCountQuery, List<GetAssetTypeSerial>>
{
    public async Task<List<GetAssetTypeSerial>> Handle(GetAssetTypeWithAssetsCountQuery request, CancellationToken cancellationToken)
    {
        var assetTypes = await dbContext.AssetTypes.GetAllAsync(cancellationToken);
        var assetSerials = await dbContext.AssetSerials.GetAllAsync(x => x.Status == Domain.Enums.AssetStatus.Available, cancellationToken);

        return assetSerials.GroupBy(x => x.Asset.AssetTypeId).Select(x => new GetAssetTypeSerial()
        {
            AssetSerials = x.Select(serial => new GetAssetSerials { Id = serial.Id, Name = serial.Asset.Name, Serial = serial.Serial, From = DateOnly.FromDateTime(serial.LastModifiedAt) }).ToList(),
            Name = assetTypes.First(at => at.Id == x.Key).Name,
            Icon = assetTypes.First(at => at.Id == x.Key).Icon
        }).OrderBy(x => x.Name).ToList();
    }
}