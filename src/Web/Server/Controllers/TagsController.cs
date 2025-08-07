using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MRA.AssetsManagement.Application.Features.Tags.Commands;
using MRA.AssetsManagement.Application.Features.Tags.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TagsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Tag>> Create(CreateTagCommand command, CancellationToken cancellationToken)
    {
        var createdTag = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBySlug), new {createdTag.Slug} ,createdTag);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetTagsQuery(), cancellationToken));
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Tag>>> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetSingleTagQuery(slug), cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<bool>> Update(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteTagCommand(id), cancellationToken);

        return NoContent();
    }
}