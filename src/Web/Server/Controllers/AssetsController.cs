using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.AssetHistories.Queries;
using MRA.AssetsManagement.Application.Features.Assets.Commands;
using MRA.AssetsManagement.Application.Features.Assets.Queries;
using MRA.AssetsManagement.Application.Features.AssetSerials.Commands;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Application.Features.Documents.Create;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerialHistory;

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

    [HttpGet("{typeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsByTypeId(string typeId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetsByTypeIdQuery(typeId), cancellationToken));
    }

    [HttpGet("serial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<GetAssetSerial>>> GetAssetSerials(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetSerialsQuery(), cancellationToken));
    }

    [HttpGet("serial/{serial}")]
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AssetSerial>> UpdateAssetSerial(UpdateAssetSerialCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
    
    [HttpPost("purchase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Document>> CreatePurchase(CreatePurchaseCommand command)
    {
        var document = await Mediator.Send(command);
        return Ok(document);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<GetAsset>> CreateAsset(CreateAssetRequest request, CancellationToken cancellationToken)
    {
        var asset = await Mediator.Send(new CreateAssetCommand(request));
        return CreatedAtAction(nameof(CreateAsset), asset);
    }
}