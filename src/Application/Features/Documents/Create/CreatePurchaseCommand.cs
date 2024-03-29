using AutoMapper;
using MediatR;
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
    private readonly IMapper _mapper;

    public CreatePurchaseCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
            if (string.IsNullOrEmpty(detail.Asset.Id))
                await CreateNewAsset(detail.Asset, cancellationToken);
            else
                detail.Asset = await _context.Assets.GetAsync(x => x.Id == detail.Asset.Id, cancellationToken);
            
            document.Details.Add(_mapper.Map<DocumentDetail>(detail));
        }

        await _context.Documents.CreateAsync(cancellationToken, document);
        return document;
    }

    private async Task CreateNewAsset(Asset asset, CancellationToken cancellationToken)
    {
        var assetType = await _context.AssetTypes.GetAsync(asset.AssetTypeId, cancellationToken);

        var assetSerial = new AssetSerial
        {
            Asset = asset,
            Status = AssetStatus.Available,
            Serial = assetType.ShortName + DateTime.Now.ToUnixTimestamp()
        };

        await _context.Assets.CreateAsync(cancellationToken, asset);
        await _context.AssetSerials.CreateAsync(cancellationToken, assetSerial);
    }
}