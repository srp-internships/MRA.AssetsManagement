using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Assets.Queries;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetAssetsQuery();
            var assets = await Mediator.Send(query, cancellationToken);
            return Ok(assets);
        }
    }
}
