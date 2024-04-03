using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public class GetAssetSerialQuery : IRequest<IEnumerable<GetAssetSerial>>
{
}

public class GetAssetSerialQueryHandler : IRequestHandler<GetAssetSerialQuery, IEnumerable<GetAssetSerial>>
{
    private readonly IApplicationDbContext _context;

    public GetAssetSerialQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAssetSerial>> Handle(GetAssetSerialQuery request,
        CancellationToken cancellationToken)
    {
        var histories = await _context.AssetHistories.GetAllAsync(cancellationToken);
        var serialWithHistories = new Dictionary<AssetSerial, IEnumerable<AssetHistory>>();

        histories.GroupBy(x => x.AssetSerial)
            .ToList()
            .ForEach(x => serialWithHistories.Add(x.Key, x.Select(z => z).OrderByDescending(h => h.DateTime)));

        var assetTypeIds = serialWithHistories.Keys.Select(x => x.Asset.AssetTypeId);
        var assetTypes = await _context.AssetTypes.GetAllAsync(x => assetTypeIds.Contains(x.Id), cancellationToken);

        var result = serialWithHistories.Select(row => new GetAssetSerial()
        {
            Status = row.Key.Status.ToString(),
            Serial = row.Key.Serial,
            AssetSerialType = new(assetTypes.First(x => x.Id == row.Key.Asset.AssetTypeId).Icon,
                assetTypes.First(x => x.Id == row.Key.Asset.AssetTypeId).Name),
            Name = row.Key.Asset.Name,
            AssignedTo = row.Value.First().Employee,
            From = row.Value.First().DateTime
        }).OrderBy(x => x.Status);

        return result;
    }
}