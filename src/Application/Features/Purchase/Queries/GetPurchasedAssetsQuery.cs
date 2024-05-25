using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.AssetsManagement.Web.Shared.Purchase;

namespace MRA.AssetsManagement.Application.Features.Purchase.Queries;

public class GetPurchasedAssetsQuery(PurchasedAssetsRequest purchasedAssetsRequest) : IRequest<List<GetPurchasedAssets>>
{
    public readonly PurchasedAssetsRequest PurchasedAssetsRequest = purchasedAssetsRequest;
}

public class GetPurchasedAssetsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetPurchasedAssetsQuery, List<GetPurchasedAssets>>
{
    public async Task<List<GetPurchasedAssets>> Handle(GetPurchasedAssetsQuery request,
        CancellationToken cancellationToken)
    {
        var documents = await context.Documents.GetAllAsync(x =>
            x.Date.Date >= request.PurchasedAssetsRequest.FromDate!.Value.Date
            && x.Date.Date <= request.PurchasedAssetsRequest.ToDate!.Value.Date.AddDays(1), cancellationToken);

        var assetsTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        List<GetPurchasedAssets> response = new();

        foreach (var document in documents)
        {
            foreach (var detail in document.Details)
            {
                var assetType =
                    mapper.Map<GetAssetType>(assetsTypes.FirstOrDefault(x => x.Id == detail.Asset.AssetTypeId));
                var asset = new GetPurchasedAssets()
                {
                    AssetName = detail.Asset.ToString(),
                    AssetType = assetType,
                    Price = detail.Price,
                    Quantity = detail.Quantity,
                    DateTime = document.Date
                };
                response.Add(asset);
            }
        }

        return response.Where(x =>
            request.PurchasedAssetsRequest.AssetTypeId is null ||
            x.AssetType.Id == request.PurchasedAssetsRequest.AssetTypeId).ToList();
    }
}