using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Common.Security;

using MRA.AssetsManagement.Application.Data;

using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Domain.Enums;
using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Commands;

public class UpdateAssetSerialCommand : IRequest<AssetSerial>
{
    public string Id { get; set; } = null!;
    public UserDisplay? UserDisplay { get; set; }
    public Web.Shared.Enums.AssetStatus Status { get; set; }
    public string? Note { get; set; }
}

public class UpdateAssetSerialComandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper) : IRequestHandler<UpdateAssetSerialCommand, AssetSerial>
{
    private readonly IMapper _mapper = mapper;

    public async Task<AssetSerial> Handle(UpdateAssetSerialCommand request, CancellationToken cancellationToken)
    {
        var assetSerial = await context.AssetSerials.GetAsync(x => x.Id == request.Id);

        assetSerial.Employee = request.UserDisplay;
        assetSerial.Status = Enum.Parse<Domain.Enums.AssetStatus>(request.Status.ToString());

        assetSerial.LastModifiedAt = DateTime.Now;
        assetSerial.LastModifiedBy = currentUserService.GetUserId().ToString();

        var history = new AssetHistory
        {
            DateTime = DateTime.Now,
            UserId = currentUserService.GetUserId().ToString(),
            HistoryAssetSerial =  _mapper.Map<HistoryAssetSerial>(assetSerial),
            Note = request.Note
        };

        await context.AssetSerials.UpdateAsync(assetSerial, cancellationToken);
        await context.AssetHistories.CreateAsync(cancellationToken, history);

        return assetSerial;
    }
}