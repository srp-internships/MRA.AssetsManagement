using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries;

public class GetPagedAssetSerialsQuery(int currentPage, int pageSize) : IRequest<PagedList<GetAssetSerial>>
{
    public int CurrentPage { get; set; } = currentPage;
    public int PageSize { get; set; } = pageSize;
}

public class GetPagedAssetSerialsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetPagedAssetSerialsQuery, PagedList<GetAssetSerial>>
{
    public async Task<PagedList<GetAssetSerial>> Handle(GetPagedAssetSerialsQuery request,
        CancellationToken cancellationToken)
    {
        var serials = await context.AssetSerials.GetPagedListAsync(request.CurrentPage, request.PageSize, cancellationToken);
        var totalCount = (await context.AssetSerials.GetAllAsync()).Count;
        var assetTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        var data = serials.Select(x => new GetAssetSerial
        {
            Id = x.Id,
            Status = Enum.Parse<AssetStatus>(x.Status.ToString()),
            Serial = x.Serial,
            Name = x.Asset.Name,
            LastModified = x.LastModifiedAt,
            From = x.CreatedAt,
            Employee = mapper.Map<UserDisplay>(x.Employee),
            AssetSerialType = new(assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Icon,
                assetTypes.First(at => at.Id == x.Asset.AssetTypeId).Name)
        }).OrderBy(x => x.Status.ToString());
        
        var result = new PagedList<GetAssetSerial> { Data = data, TotalCount = totalCount};

        return result;
    }
}