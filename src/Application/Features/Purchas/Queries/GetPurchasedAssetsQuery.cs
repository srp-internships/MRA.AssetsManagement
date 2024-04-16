using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Purchas;

namespace MRA.AssetsManagement.Application.Features.Purchas.Queries;

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

        var listOfDocuments = documents
            .Where(x => x.Date.Date >= request.PurchasedAssetsRequest.FromDate!.Value.Date
                        && x.Date.Date <= request.PurchasedAssetsRequest.ToDate!.Value.Date)
            .ToList();

        var assetsTypes = await context.AssetTypes.GetAllAsync(cancellationToken);

        var response = new List<GetPurchasedAssets?>();

        foreach (var document in listOfDocuments)
        {
            response.AddRange(from detail in document.Details 
                where request.PurchasedAssetsRequest.AssetTypeId is null 
                      || detail.Asset.AssetTypeId == request.PurchasedAssetsRequest.AssetTypeId 
                select new GetPurchasedAssets() 
                    { AssetName = detail.Asset.Name, AssetType = assetsTypes
                        .FirstOrDefault(x => x.Id == detail.Asset.AssetTypeId)?
                        .Name, Price = detail.Price, Quantity = detail.Quantity });
        }
        var uniqueAssets = response
            .Where(asset => asset != null)
            .GroupBy(asset => new {asset?.AssetType })
            .Select(group =>
            {
                var totalPrice = group.Sum(asset => asset!.Price * asset.Quantity);
                return new GetPurchasedAssets
                {
                    AssetName = group.First()?.AssetName,
                    AssetType = group.First()?.AssetType,
                    Quantity = group.Sum(asset => asset!.Quantity),
                    Price = totalPrice
                };
            })
            .ToList();
        return uniqueAssets;
    }
}