using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.Documents.Create;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[Authorize]
public class DocumentsController : ApiControllerBase
{
    [HttpPost("purchase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Document>> CreatePurchase(CreatePurchaseCommand command)
    {
        var document = await Mediator.Send(command);
        return Ok(document);
    }
}