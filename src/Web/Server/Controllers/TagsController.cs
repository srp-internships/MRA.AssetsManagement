using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Tags.Commands;
using MRA.AssetsManagement.Application.Features.Tags.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[Authorize(Roles = "SuperAdmin, ApplicationAdmin")]
public class TagsController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Tag>> Create(CreateTagCommand command, CancellationToken cancellationToken)
    {
        var createdTag = await Mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBySlug), new {createdTag.Slug} ,createdTag);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTagsQuery(), cancellationToken));
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Tag>>> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSingleTagQuery(slug), cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<bool>> Update(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteTagCommand(id), cancellationToken);

        return NoContent();
    }
}