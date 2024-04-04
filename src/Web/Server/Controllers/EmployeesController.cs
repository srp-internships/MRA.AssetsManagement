using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Employees.Command;
using MRA.AssetsManagement.Application.Features.Employees.Queries;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    
    [HttpGet("id/{id}")]
    public async Task<ActionResult<Employee>> GetById(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeeByIdQuery(id), cancellationToken);
    }
    [HttpGet("email/{email}")]
    public async Task<ActionResult<Employee>> GetByEmail(string email,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeeByEmailQuery(email),cancellationToken);
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command,cancellationToken);
    }
}