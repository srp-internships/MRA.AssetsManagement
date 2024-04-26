using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public class GetAssetSerialsQuery : IRequest<IEnumerable<GetAssetSerial>>
{
}

public class GetAssetSerialsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetAssetSerialsQuery, IEnumerable<GetAssetSerial>>
{
    public async Task<IEnumerable<GetAssetSerial>> Handle(GetAssetSerialsQuery request,
        CancellationToken cancellationToken)
    {
        var serials = await context.AssetSerials.GetAllAsync(cancellationToken);
        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        return serials.Select(x => new GetAssetSerial
        {
            Id = x.Id,
            Status = Enum.Parse<AssetStatus>(x.Status.ToString()),
            Serial = x.Serial,
            Name = x.Asset.Name,
            AssetFullName = x.Asset.ToString(),
            LastModified = x.LastModifiedAt,
            From = x.CreatedAt,
            Employee = mapper.Map<UserDisplay>(x.Employee),
            AssetSerialType = new(assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Icon,
                assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name)
        }).OrderBy(x => x.Status.ToString());
    }
}