using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Application.Features.AssetTypes.Queries;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Server.Controllers;
[Authorize]
public class AssetTypesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAssetType>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAssetTypesQuery(), cancellationToken));
    }

    [HttpGet("serials")]
    public async Task<ActionResult<IEnumerable<GetAssetTypeSerial>>> GetAvailableAssetsWithAssetType(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAvailableAssetsWithAssetTypesQuery(), cancellationToken));
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<IEnumerable<GetAssetType>>> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSingleAssetTypeQuery(slug), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<GetAssetType>> Create(CreateAssetTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateAssetTypeCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetBySlug), new { result.Slug }, result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(UpdateAssetTypeCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return NoContent();
    }
}