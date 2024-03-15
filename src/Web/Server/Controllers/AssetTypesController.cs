using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Application.Features.AssetTypes.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

public class AssetTypesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetType>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetTypesQuery(), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AssetType>> Create(CreateAssetTypeCommand command, CancellationToken cancellationToken)
    {
        var uri = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/" +
                          $"{ControllerContext.ActionDescriptor.ControllerName}");

        return Created(uri, await Mediator.Send(command, cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(UpdateAssetTypeCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("archive/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Archive(string id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ArchiveAssetTypeCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPatch("restore/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Restore(string id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new RestoreAssetTypeCommand(id), cancellationToken);
        return NoContent();
    }
}