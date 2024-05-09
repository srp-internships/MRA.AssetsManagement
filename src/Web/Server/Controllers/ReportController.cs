using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Purchase.Queries;
using MRA.AssetsManagement.Web.Shared.Purchase;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[Authorize]
public class ReportController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetPurchasedAssets>>> GetPurchases([FromQuery] PurchasedAssetsRequest purchasedAssetsRequest,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPurchasedAssetsQuery(purchasedAssetsRequest),cancellationToken);
    }
}