using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.Assets.Queries;
using MRA.AssetsManagement.Application.Features.AssetSerials.Commands;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[Authorize]
public class AssetsController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Asset>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetsQuery(), cancellationToken));
    }

    [HttpGet("serial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetSerialQuery(), cancellationToken));
    }

    [HttpGet("serial/{serial}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(string serial, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetSerialBySerialQuery(serial), cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AssetSerial>> UpdateAssetSerial(UpdateAssetSerialCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}