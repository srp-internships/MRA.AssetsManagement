using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Web.Server.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class HomeController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAssetTypeSerialDto>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetMyAssetTypesQuery(), cancellationToken));
        }
    }

}
