using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.Tags.Commands;
using MRA.AssetsManagement.Application.Features.Tags.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

public class TagController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Tag>> Create(CreateTagCommand command, CancellationToken cancellationToken)
    {
        var createdTag = await Mediator.Send(command, cancellationToken);
        var uri = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/" +
                          $"{ControllerContext.ActionDescriptor.ControllerName}");

        return Created(uri, createdTag);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTagsQuery(), cancellationToken));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteTagCommand(id), cancellationToken);
        return Ok();
    }
}