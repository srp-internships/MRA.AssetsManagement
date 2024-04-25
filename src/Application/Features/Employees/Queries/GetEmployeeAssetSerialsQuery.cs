using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;

public class GetEmployeeAssetSerialsQuery(string userName) : IRequest<IEnumerable<GetAssetSerials>>
{
    public string UserName => userName;
}

public class GetEmployeeAssetSerialsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetEmployeeAssetSerialsQuery, IEnumerable<GetAssetSerials>>
{
    public async Task<IEnumerable<GetAssetSerials>> Handle(GetEmployeeAssetSerialsQuery request, CancellationToken cancellationToken)
    {
        var assetSerials = await context.AssetSerials.GetAllAsync(x => x.Employee != null &&
                                                                    x.Employee.UserName == request.UserName &&
                                                                    x.Status == Domain.Enums.AssetStatus.Taken, cancellationToken);

        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        return assetSerials.Select(x =>
            new GetAssetSerials
            {
                Id = x.Id,
                Name = x.Asset.Name,
                Serial = x.Serial,
                Type = new Web.Shared.Assets.AssetSerialType(assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Icon, assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name),
                From = DateOnly.FromDateTime(x.LastModifiedAt),
            }).ToList();
    }
}