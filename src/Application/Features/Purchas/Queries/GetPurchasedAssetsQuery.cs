using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Purchas;

namespace MRA.AssetsManagement.Application.Features.Purchas.Queries;

public class GetPurchasedAssetsQuery : IRequest<List<GetPurchasedAssets?>>
{
    public readonly PurchasedAssetsRequest PurchasedAssetsRequest;

    public GetPurchasedAssetsQuery(PurchasedAssetsRequest purchasedAssetsRequest)
    {
        PurchasedAssetsRequest = purchasedAssetsRequest;
    }
}

public class GetPurchasedAssetsQueryHandler : IRequestHandler<GetPurchasedAssetsQuery, List<GetPurchasedAssets?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPurchasedAssetsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetPurchasedAssets?>> Handle(GetPurchasedAssetsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _context.Documents.GetAllAsync(cancellationToken);

        var listOfDocuments = documents
            .Where(x => x.Date >= request.PurchasedAssetsRequest.FromDate
                        && x.Date <= request.PurchasedAssetsRequest.ToDate)
            .ToList();

        var serials = await _context.AssetSerials.GetAllAsync(cancellationToken);
        var assetsTypes = await _context.AssetTypes.GetAllAsync(cancellationToken);

        return (from document in listOfDocuments
            from detail in document.Details
            where request.PurchasedAssetsRequest.AssetId is null || detail.Asset.Id == request.PurchasedAssetsRequest.AssetId
            let asserSerial = serials.Where(x => x.Asset.Id == detail.Asset.Id).ToList()
            from serial in asserSerial
            select new GetPurchasedAssets()
            {
                AssetName = detail.Asset.Name,
                AssetSerial = serial.Serial,
                AssetType = assetsTypes.FirstOrDefault(x => x.Id == detail.Asset.AssetTypeId)?.Name,
                Price = detail.Price / asserSerial.Count(),
                Quantity = 1
            }).ToList();
    }
}