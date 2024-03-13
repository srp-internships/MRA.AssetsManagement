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
    public async Task<IEnumerable<AssetType>> Get()
    {
        return await _mediator.Send(new GetAssetTypesQuery());
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(AssetType type)
    {
        await _mediator.Send(new CreateAssetTypeCommand(type));
        return Ok();
    }
}