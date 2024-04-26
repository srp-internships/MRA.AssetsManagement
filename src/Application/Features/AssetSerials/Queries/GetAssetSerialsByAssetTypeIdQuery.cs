using System.Security.Cryptography.Xml;

using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;

using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public class GetAssetSerialsByAssetTypeIdQuery(string assetTypeId) : IRequest<IEnumerable<GetAssetSerial>>
{
    public string AssetTypeId => assetTypeId;
}

public class GetAssetSerialsByAssetTypeIdQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetAssetSerialsByAssetTypeIdQuery, IEnumerable<GetAssetSerial>>
{
    public async Task<IEnumerable<GetAssetSerial>> Handle(GetAssetSerialsByAssetTypeIdQuery request, CancellationToken cancellationToken)
    {
        var assetSerials = await dbContext.AssetSerials.GetAllAsync(x => x.Asset.AssetTypeId == request.AssetTypeId &&
                                                                    x.Status == Domain.Enums.AssetStatus.Available, cancellationToken);

        return assetSerials.Select(x => new GetAssetSerial()
        {
            Id = x.Id,
            Serial = x.Serial,
            Name = x.Asset.Name
        });
    }
}