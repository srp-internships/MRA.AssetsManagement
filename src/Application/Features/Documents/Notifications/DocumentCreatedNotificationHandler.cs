using MediatR;
using MRA.AssetsManagement.Application.Common.Security;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Documents.Notifications;

public class DocumentCreatedNotificationHandler : INotificationHandler<DocumentCreatedNotification>
{
    private readonly ICurrentUserService _service;
    private readonly IApplicationDbContext _context;

    public DocumentCreatedNotificationHandler(ICurrentUserService service, IApplicationDbContext context)
    {
        _service = service;
        _context = context;
    }

    public async Task Handle(DocumentCreatedNotification notification, CancellationToken cancellationToken)
    {
        var assetSerials = await _context.AssetSerials
            .GetAllAsync(x => notification.Document.Details.Any(d => x.Asset.Id == d.Id),
                cancellationToken);

        foreach (var assetSerial in assetSerials)
        {
            var history = new AssetHistory
            {
                AssetSerial = assetSerial,
                DateTime = notification.Document.Date,
                Employee = null,
                UserId = _service.GetUserId().ToString()
            };
            await _context.AssetHistories.CreateAsync(cancellationToken, history);
        }
    }
}