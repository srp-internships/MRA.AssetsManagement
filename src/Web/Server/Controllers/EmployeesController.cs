using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Employees.Handlers;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public EmployeesController(IMediator mediator,IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }
    
    [HttpGet("getAll")]
    [Authorize]
    public async Task<ActionResult<List<EmployeeResponse>>> GetAll()
    {
        return await _mediator.Send(new GetEmployeesQuery());
    }
    
    [HttpGet("getById")]
    [Authorize]
    public async Task<ActionResult<EmployeeResponse>> GetById(string id)
    {
        return await _mediator.Send(new GetEmployeeByIdQuery(id));
    }
    [HttpGet("getByEmail")]
    [Authorize]
    public async Task<ActionResult<EmployeeResponse>> GetByEmail(string email)
    {
        return await _mediator.Send(new GetEmployeeByEmailQuery(email));
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<string>> Create(RegisterEmployee registerEmployee)
    {
        return await _mediator.Send(new CreateEmployeeCommand(registerEmployee));
    }
}