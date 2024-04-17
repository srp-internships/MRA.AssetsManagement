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

        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        return assetSerials.Select(x =>
            new GetEmployeeAssetSerials
            {
                AssetName = x.Asset.Name,
                Serial = x.Serial,
                From = DateOnly.FromDateTime(x.LastModifiedAt),
                AssetType = assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name
            }).ToList();
    }
}