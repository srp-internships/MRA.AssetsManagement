using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Purchas;

namespace MRA.AssetsManagement.Application.Features.Purchase.Queries;

public class GetPurchasedAssetsQuery(PurchasedAssetsRequest purchasedAssetsRequest) : IRequest<List<GetPurchasedAssets>>
{
    public readonly PurchasedAssetsRequest PurchasedAssetsRequest = purchasedAssetsRequest;
} 
public class GetPurchasedAssetsQueryHandler(IApplicationDbContext context)
        : IRequestHandler<GetPurchasedAssetsQuery, List<GetPurchasedAssets>>
    {
        public async Task<List<GetPurchasedAssets>> Handle(GetPurchasedAssetsQuery request, CancellationToken cancellationToken)
        {
            var documents = await context.Documents.GetAllAsync(cancellationToken);

            var assetsTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

            var uniqueAssets = documents
                .Where(x => x.Date.Date >= request.PurchasedAssetsRequest.FromDate!.Value.Date
                            && x.Date.Date <= request.PurchasedAssetsRequest.ToDate!.Value.Date)
                .SelectMany(document => document.Details)
                .Where(detail => request.PurchasedAssetsRequest.AssetTypeId == null || detail.Asset.AssetTypeId == request.PurchasedAssetsRequest.AssetTypeId)
                .GroupBy(detail => detail.Asset.Name)
                .Select(group =>
                {
                    var totalPrice = group.Sum(detail => detail.Price);
                    var assetType = assetsTypes.FirstOrDefault(x => x.Id == group.First().Asset.AssetTypeId);
                    return new GetPurchasedAssets
                    {
                        AssetName = group.Key,
                        AssetType = assetType!.Name,
                        Quantity = group.Sum(detail => detail.Quantity),
                        Price = totalPrice
                    };
                })
                .ToList();

            return uniqueAssets;
        }
    }
