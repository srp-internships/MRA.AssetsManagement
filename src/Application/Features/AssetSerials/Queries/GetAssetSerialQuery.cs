using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public class GetAssetSerialQuery : IRequest<IEnumerable<GetAssetSerial>>
{
}

public class GetAssetSerialQueryHandler(IApplicationDbContext context, IEmployeeService employeeService, IMapper mapper)
    : IRequestHandler<GetAssetSerialQuery, IEnumerable<GetAssetSerial>>
{
    public async Task<IEnumerable<GetAssetSerial>> Handle(GetAssetSerialQuery request,
        CancellationToken cancellationToken)
    {
        var serials = await context.AssetSerials.GetAllAsync(cancellationToken);
        var employees = await employeeService.GetAll();
        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        return serials.Select(x => new GetAssetSerial
        {
            Id = x.Id,
            Status = x.Status.ToString(),
            Serial = x.Serial,
            Name = x.Asset.Name,
            LastModified = x.LastModifiedAt,
            From = x.CreatedAt,
            Employee = mapper.Map<UserDisplay>(x.Employee),
            AssetSerialType = new(assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Icon,
                assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name)
        }).OrderBy(x => x.Status);
    }
}