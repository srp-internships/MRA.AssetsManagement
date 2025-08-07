using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Application.Features.AssetTypes.Queries;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssetTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAssetType>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAssetTypesQuery(), cancellationToken));
    }

    [HttpGet("serials")]
    public async Task<ActionResult<IEnumerable<GetAssetTypeSerial>>> GetAvailableAssetsWithAssetType(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAvailableAssetsWithAssetTypesQuery(), cancellationToken));
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<IEnumerable<GetAssetType>>> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetSingleAssetTypeQuery(slug), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<GetAssetType>> Create(CreateAssetTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateAssetTypeCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetBySlug), new { result.Slug }, result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<bool>> Update(UpdateAssetTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}