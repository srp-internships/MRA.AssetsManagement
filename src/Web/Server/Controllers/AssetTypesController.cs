using MediatR;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Application.Features.AssetTypes.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetType>>> Get()
    {
        return Ok(await _mediator.Send(new GetAssetTypesQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAssetTypeCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateAssetTypeCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPatch("archive/{id}")]
    public async Task<IActionResult> Archive(string id)
    {
        await _mediator.Send(new ArchiveAssetTypeCommand(id));
        return Ok();
    }
    
    [HttpPatch("restore/{id}")]
    public async Task<IActionResult> Restore(string id)
    {
        await _mediator.Send(new RestoreAssetTypeCommand(id));
        return Ok();
    }
}