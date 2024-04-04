using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Application.Features.AssetTypes.Queries;
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<GetAssetType>>> GetById(string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSingleAssetTypeQuery(id), cancellationToken));
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<GetAssetType>> Create(CreateAssetTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateAssetTypeCommand(request), cancellationToken);
        //REVIEW: MPT-59
        return Ok(result);
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