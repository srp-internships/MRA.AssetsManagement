using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public record GetAssetSerialBySerialQuery(string Serial) : IRequest<GetAssetSerial>;

public class GetAssetSerialBySerialQueryHandler(IApplicationDbContext context,
                                                IEmployeeService employeeService,
                                                IMapper mapper) : IRequestHandler<GetAssetSerialBySerialQuery, GetAssetSerial>
{
    public async Task<GetAssetSerial> Handle(GetAssetSerialBySerialQuery request, CancellationToken cancellationToken)
    {
        var serial = await context.AssetSerials.GetAsync(x => x.Serial == request.Serial, cancellationToken);
        var employees = await employeeService.GetAll();
        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        return new GetAssetSerial
        {
            Id = serial.Id,
            Status = serial.Status.ToString(),
            Serial = serial.Serial,
            Name = serial.Asset.Name,
            LastModified = serial.LastModifiedAt,
            Employee = mapper.Map<UserDisplay>(serial.Employee),
            AssetSerialType = new(assetTypes.First(at => at.Id == serial.Asset.AssetTypeId).Icon,
                assetTypes.First(at => at.Id == serial.Asset.AssetTypeId).Name),
            From = serial.CreatedAt
        };
    }
}