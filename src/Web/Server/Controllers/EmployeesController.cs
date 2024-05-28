using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Employees.Queries;
using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Infrastructure;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[Authorize(Roles = "SuperAdmin, ApplicationAdmin")]
public class EmployeesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeesQuery(),cancellationToken);
    }
    
    [HttpGet("{userName}")]
    public async Task<ActionResult<Employee?>> GetByUserName(string userName, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeeByUserNameQuery(userName), cancellationToken);
    }
    [HttpGet("email/{email}")]
    public async Task<ActionResult<Employee>> GetByEmail(string email,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeeByEmailQuery(email),cancellationToken);
    }
    
    [HttpPost]
    [Authorize(ApplicationPolicies.Administrator)]
    public async Task<ActionResult<GetEmployee>> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateEmployeeCommand(request),cancellationToken);
    }

    [HttpGet("serials/{userName}")]
    public async Task<ActionResult<IEnumerable<GetAssetSerials>>> GetEmployeeAssetSerials(string userName, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetEmployeeAssetSerialsQuery(userName), cancellationToken));
    }
}