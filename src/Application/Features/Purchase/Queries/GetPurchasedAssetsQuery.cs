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
            var documents = await context.Documents.GetAllAsync(x=>
                x.Date.Date >= request.PurchasedAssetsRequest.FromDate!.Value.Date
                && x.Date.Date <= request.PurchasedAssetsRequest.ToDate!.Value.Date.AddDays(1),cancellationToken);

            var assetsTypes = await context.AssetTypes.GetAllAsync(cancellationToken);
            
            List<GetPurchasedAssets> response = new();
            
            foreach (var document in documents)
            {
                foreach (var detail in document.Details)
                {
                    var asset = new GetPurchasedAssets()
                    {
                        AssetName = detail.Asset.Name,
                        Price = detail.Price,
                        Quantity = detail.Quantity,
                        DateTime = document.Date
                    };
                    if (request.PurchasedAssetsRequest.AssetTypeId == null)
                    {
                        asset.AssetType = assetsTypes.FirstOrDefault(x => x.Id == detail.Asset.AssetTypeId)?.Name;
                    }

                    if (detail.Asset.AssetTypeId == request.PurchasedAssetsRequest.AssetTypeId)
                    {
                        asset.AssetType = assetsTypes.FirstOrDefault(x => x.Id == detail.Asset.AssetTypeId)?.Name;
                    }
                    response.Add(asset);
                }
            }
            return response;
        }
    }
