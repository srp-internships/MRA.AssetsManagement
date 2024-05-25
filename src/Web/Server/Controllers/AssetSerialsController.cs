using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.AssetHistories.Queries;

using MRA.AssetsManagement.Application.Features.AssetSerials.Commands;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerialHistory;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[Authorize(Roles = "SuperAdmin, ApplicationAdmin")]
public class AssetSerialsController : ApiControllerBase
{
    [HttpPatch("set-tag")]
    public async Task<ActionResult<IEnumerable<GetTag>>> SetTagToAssetSerial(SetTagsToAssetSerialsRequest request)
    {
        return Ok(await Mediator.Send(new SetTagsToAssetSerialCommand(request)));
    }

    [HttpGet("serial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetSerialsQuery(), cancellationToken));
    }

    [HttpGet("{serial}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(string serial, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSingleAssetSerialQuery(serial), cancellationToken));
    }

    [HttpGet("histories/{serial}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerialHistory>>> GetAssetSerialHistories(string serial, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetHistoryForSerialQuery(serial), cancellationToken));
    }
}