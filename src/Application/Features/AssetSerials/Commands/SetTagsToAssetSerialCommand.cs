using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Commands;

public record SetTagsToAssetSerialCommand(SetTagsToAssetSerialsRequest Request) : IRequest<IEnumerable<GetTag>>;

public class SetTagToAssetSerialCommandHandler(IApplicationDbContext context) : IRequestHandler<SetTagsToAssetSerialCommand, IEnumerable<GetTag>>
{
    public async Task<IEnumerable<GetTag>> Handle(SetTagsToAssetSerialCommand request, CancellationToken cancellationToken)
    {
        var assetSerial = await context.AssetSerials.GetAsync(request.Request.AssetSerialId, cancellationToken);

        var tag = await context.Tags.GetAsync(request.Request.TagId, cancellationToken);

        if (request.Request.IsAdd)
            assetSerial.Tags.Add(tag);
        else
            assetSerial.Tags.Remove(assetSerial.Tags.First(x => x.Id == request.Request.TagId));

        await context.AssetSerials.UpdateAsync(assetSerial, cancellationToken);

        return assetSerial.Tags.Select(x => new GetTag {Id = x.Id, Color = x.Color!, Name = x.Name});
    }
}