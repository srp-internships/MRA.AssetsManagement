using AutoMapper;
using MediatR;

using MRA.AssetsManagement.Application.Common.Security;
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
    private readonly ICurrentUserService _currentUserService;

    public CreatePurchaseCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
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

        List<AssetHistory> histories = [];
        List<AssetSerial> assetSerials = [];
        
        foreach (var detail in request.Details)
        {
            if (string.IsNullOrEmpty(detail.Asset.Id))
                await _context.Assets.CreateAsync(cancellationToken, detail.Asset);
            else
                detail.Asset = await _context.Assets.GetAsync(x => x.Id == detail.Asset.Id, cancellationToken);
            
            var assetType = await _context.AssetTypes.GetAsync(detail.Asset.AssetTypeId, cancellationToken);
            
            var assetSerial = new AssetSerial
            {
                Asset = detail.Asset,
                Status = AssetStatus.Available,
                Serial = assetType.ShortName + DateTime.Now.ToUnixTimestamp()
            };

            var history = new AssetHistory()
            {
                AssetSerial = assetSerial,
                DateTime = DateTime.Now,
                Employee = null,
                UserId = _currentUserService.GetUserId().ToString()
            };

            document.Details.Add(_mapper.Map<DocumentDetail>(detail));
            assetSerials.Add(assetSerial);
            histories.Add(history);
        }

        await _context.Documents.CreateAsync(cancellationToken, document);
        await _context.AssetSerials.CreateAsync(cancellationToken, assetSerials.ToArray());
        await _context.AssetHistories.CreateAsync(cancellationToken, histories.ToArray());

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
    }
}