using MediatR;
using MRA.AssetsManagement.Application.Common.Exceptions;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Domain.Enums;
using MRApiCommon.Extensions;

namespace MRA.AssetsManagement.Application.Features.Documents.Create;

public class CreatePurchaseCommand : CreateDocumentCommand, IRequest<PurchaseDocument>
{
    public string Vendor { get; set; } = null!;
}

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, PurchaseDocument>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreatePurchaseCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<PurchaseDocument> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var document = new PurchaseDocument()
        {
            Date = request.Date,
            Approved = request.Approved,
            Vendor = request.Vendor,
            Details = new List<DocumentDetail>()
        };

        foreach (var detail in request.Details)
        {
            Asset? asset;
            
            if (detail.AssetId is not null)
            {
                asset = await _context.Assets.GetAsync(detail.AssetId, cancellationToken);

                if (asset is null)
                    throw new NotFoundEntityException(nameof(Asset), detail.AssetId);
            }
            else
                asset = await CreateNewAsset(detail, cancellationToken);
            
            document.Details.Add(new DocumentDetail
            {
                Asset = asset, Id = asset.Id, Price = detail.Price, Quantity = detail.Quantity
            });
        }

        await _context.Documents.CreateAsync(cancellationToken, document);
        return document;
    }

    private async Task<Asset> CreateNewAsset(CreateDocumentDetailCommand detail, CancellationToken cancellationToken)
    {
        var assetType = await _context.AssetTypes.GetAsync(detail.AssetTypeId!, cancellationToken);

        if (assetType is null)
            throw new NotFoundEntityException(nameof(Asset), detail.AssetTypeId!);

        var asset = new Asset { Name = detail.AssetName!, AssetTypeId = detail.AssetTypeId!, AssetType = assetType };

        var assetSerial = new AssetSerial
        {
            Asset = asset,
            Status = AssetStatus.Available,
            Serial = asset.AssetType.ShortName + DateTime.Now.ToUnixTimestamp()
        };

        await _context.Assets.CreateAsync(cancellationToken, asset);
        await _context.AssetSerials.CreateAsync(cancellationToken, assetSerial);

        return asset;
    }
}