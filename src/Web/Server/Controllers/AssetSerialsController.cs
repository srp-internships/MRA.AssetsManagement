using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using MRA.AssetsManagement.Application.Features.AssetHistories.Queries;

using MRA.AssetsManagement.Application.Features.AssetSerials.Commands;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerialHistory;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssetSerialsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetSerialsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("set-tag")]
    public async Task<ActionResult<IEnumerable<GetTag>>> SetTagToAssetSerial(SetTagsToAssetSerialsRequest request)
    {
        return Ok(await _mediator.Send(new SetTagsToAssetSerialCommand(request)));
    }

    [HttpGet("serial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAssetSerialsQuery(), cancellationToken));
    }

    [HttpGet("{serial}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(string serial, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetSingleAssetSerialQuery(serial), cancellationToken));
    }

    [HttpGet("histories/{serial}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerialHistory>>> GetAssetSerialHistories(string serial, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAssetHistoryForSerialQuery(serial), cancellationToken));
    }
}