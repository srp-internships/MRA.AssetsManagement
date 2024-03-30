using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Tags.Commands;
using MRA.AssetsManagement.Application.Features.Tags.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

public class TagsController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Tag>> Create(CreateTagCommand command, CancellationToken cancellationToken)
    {
        var createdTag = await Mediator.Send(command, cancellationToken);
        var uri = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/" +
                          $"{ControllerContext.ActionDescriptor.ControllerName}");

        return Created(uri, createdTag);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTagsQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Tag>>> GetById(string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSingleTagQuery(id), cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return NoContent();
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