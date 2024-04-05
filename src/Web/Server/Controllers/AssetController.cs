using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Assets.Queries;
using MRA.AssetsManagement.Application.Features.Documents.Create;
using MRA.AssetsManagement.Domain.Entities;

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
    [HttpPost("purchase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Document>> CreatePurchase(CreatePurchaseCommand command)
    {
        var document = await Mediator.Send(command);
        return Ok(document);
    }
}