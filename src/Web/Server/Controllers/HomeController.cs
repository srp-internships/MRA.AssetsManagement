using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MRA.AssetsManagement.Application.Features.AssetSerials.Queries;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Web.Server.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAssetTypeSerial>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetMyAssetTypesQuery(), cancellationToken));
        }
    }
}
