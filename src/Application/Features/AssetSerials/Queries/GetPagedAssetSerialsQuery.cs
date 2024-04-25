using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public class GetPagedAssetSerialsQuery(AssetsFilterOptions assetsFilterOptions) : IRequest<PagedList<GetAssetSerial>>
{
    public AssetsFilterOptions AssetsFilterOptions { get; } = assetsFilterOptions;
}

public class GetPagedAssetSerialsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetPagedAssetSerialsQuery, PagedList<GetAssetSerial>>
{
    public async Task<PagedList<GetAssetSerial>> Handle(GetPagedAssetSerialsQuery request,
        CancellationToken cancellationToken)
    {
        var tags = new List<string>();
        
        if (request.AssetsFilterOptions.Tags != null)
            tags = request.AssetsFilterOptions.Tags.Split(",").ToList();

        Expression<Func<AssetSerial, bool>> filter = x =>
        (
            (request.AssetsFilterOptions.Status == null
            || x.Status.ToString() == request.AssetsFilterOptions.Status)) &&

            (request.AssetsFilterOptions.Tags == null ||
            x.Tags.Any(tag => tags.Any(tagName => tagName.Contains(tag.Slug)))) &&

            (request.AssetsFilterOptions.AssetName == null
            || x.Asset.Name.ToLower().Contains(request.AssetsFilterOptions.AssetName.ToLower())) &&

            (request.AssetsFilterOptions.Serial == null
            || x.Serial.ToLower().Contains(request.AssetsFilterOptions.Serial.ToLower())) &&

            (request.AssetsFilterOptions.Type == null
            || x.Asset.AssetTypeId.ToLower().Contains(request.AssetsFilterOptions.Type.ToLower()));     

        var serials = await context.AssetSerials.GetPagedListAsync(filter, request.AssetsFilterOptions.CurrentPage,
                                                                   request.AssetsFilterOptions.PageSize, cancellationToken);

        var totalCount = (await context.AssetSerials.GetAllAsync(filter)).Count;
        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        var data = serials.Select(x => new GetAssetSerial
        {
            Id = x.Id,
            Status = Enum.Parse<AssetStatus>(x.Status.ToString()),
            Serial = x.Serial,
            Name = x.Asset.Name,
            LastModified = x.LastModifiedAt,
            From = x.CreatedAt,
            Employee = mapper.Map<Web.Shared.Employees.UserDisplay>(x.Employee),
            AssetSerialType = new(assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Icon,
                assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name)
        });

        var result = new PagedList<GetAssetSerial> { Data = data, TotalCount = totalCount};

        return result;
    }
}