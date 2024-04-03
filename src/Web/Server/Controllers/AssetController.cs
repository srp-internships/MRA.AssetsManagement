using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Assets.Queries;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Web.Server.Controllers;

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
}