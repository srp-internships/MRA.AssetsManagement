using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Assets.Queries;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Application.Features.Documents.Create;
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

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsById(string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetsByIdQuery(id), cancellationToken));
    }

    [HttpGet("serial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetSerialQuery(), cancellationToken));
    }
    [HttpPost("purchase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Document>> CreatePurchase(CreatePurchaseCommand command, CancellationToken cancellationToken)
    {
        var document = await Mediator.Send(command);
        return Ok(document);
    }
}