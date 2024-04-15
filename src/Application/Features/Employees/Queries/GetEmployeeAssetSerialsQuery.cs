using MediatR;

using MRA.AssetsManagement.Application.Data;

using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;

public class GetEmployeeAssetSerialsQuery(string userName) : IRequest<IEnumerable<GetEmployeeAssetSerials>>
{
    public string UserName => userName;
}

public class GetEmployeeAssetSerialsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetEmployeeAssetSerialsQuery, IEnumerable<GetEmployeeAssetSerials>>
{
    public async Task<IEnumerable<GetEmployeeAssetSerials>> Handle(GetEmployeeAssetSerialsQuery request, CancellationToken cancellationToken)
    {
        var assetSerials = await context.AssetSerials.GetAllAsync(x => x.Employee != null &&
                                                                    x.Employee.UserName == request.UserName &&
                                                                    x.Status == Domain.Enums.AssetStatus.Taken);
        var history = (await context.AssetHistories.GetAllAsync(x => assetSerials.Contains(x.AssetSerial) && x.AssetSerial.Status == Domain.Enums.AssetStatus.Taken))
                                                    .OrderByDescending(x => x.AssetSerial.LastModifiedAt);
        var assetTypeIds = assetSerials.Select(x => x.Asset.AssetTypeId);
        var assetTypes = await context.AssetTypes.GetAllAsync(x => assetTypeIds.Contains(x.Id));

        return assetSerials.Select(x =>
            new GetEmployeeAssetSerials
            {
                AssetName = x.Asset.Name,
                Serial = x.Serial,
                From = DateOnly.FromDateTime(history.First(h => h.AssetSerial.Id == x.Id).DateTime),
                AssetType = assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name
            }).ToList();
    }
}