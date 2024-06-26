using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.AssetsManagement.Web.Shared.Enums;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public record GetSingleAssetSerialQuery(string Serial) : IRequest<GetAssetSerial>;

public class GetSingleAssetSerialQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetSingleAssetSerialQuery, GetAssetSerial>
{
    public async Task<GetAssetSerial> Handle(GetSingleAssetSerialQuery request, CancellationToken cancellationToken)
    {
        var serial = await context.AssetSerials.GetAsync(x => x.Serial == request.Serial, cancellationToken);
        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        return new GetAssetSerial
        {
            Id = serial.Id,
            Status = Enum.Parse<AssetStatus>(serial.Status.ToString()),
            Serial = serial.Serial,
            Name = serial.Asset.Name,
            AssetFullName = serial.Asset.ToString(),
            Tags = serial.Tags.Select(x => new GetTag { Id = x.Id, Name = x.Name, Color = x.Color! }).ToList(),
            Properties = serial.Asset.Properties.Select(x => new Web.Shared.AssetTypes.Properties { Label = x.Label, Value = x.Value}).ToList(),
            LastModified = serial.LastModifiedAt,
            Employee = mapper.Map<UserDisplay>(serial.Employee),
            AssetSerialType = new(assetTypes.First(at => at.Id == serial.Asset.AssetTypeId).Icon,
                assetTypes.First(at => at.Id == serial.Asset.AssetTypeId).Name),
            From = serial.CreatedAt
        };
    }
}