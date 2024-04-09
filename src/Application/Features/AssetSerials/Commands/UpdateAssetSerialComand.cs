using MediatR;

using MRA.AssetsManagement.Application.Common.Security;

using MRA.AssetsManagement.Application.Data;

using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Domain.Enums;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Commands;

public class UpdateAssetSerialCommand : IRequest<AssetSerial>
{
    public string Id { get; set; } = null!;
    public UserDisplay? UserDisplay { get; set; }
    public string Status { get; set; } = null!;
}

public class UpdateAssetSerialComandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) : IRequestHandler<UpdateAssetSerialCommand, AssetSerial>
{
    public async Task<AssetSerial> Handle(UpdateAssetSerialCommand request, CancellationToken cancellationToken)
    {
        var assetSerial = await context.AssetSerials.GetAsync(x => x.Id == request.Id);

        assetSerial.Employee = request.UserDisplay;
        assetSerial.Status = Enum.Parse<AssetStatus>(request.Status);

        assetSerial.LastModifiedAt = DateTime.Now;
        assetSerial.LastModifiedBy = currentUserService.GetUserId().ToString();

        var history = new AssetHistory
        {
            DateTime = DateTime.Now,
            UserId = currentUserService.GetUserId().ToString(),
            AssetSerial = assetSerial
        };

        await context.AssetHistories.CreateAsync(cancellationToken, history);
        await context.AssetSerials.UpdateAsync(assetSerial, cancellationToken);

        return assetSerial;
    }
}