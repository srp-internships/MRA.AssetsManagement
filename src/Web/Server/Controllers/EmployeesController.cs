using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Employees.Queries;
using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Infrastructure;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using AutoMapper;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public EmployeesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeesQuery(),cancellationToken);
    }
    
    [HttpGet("debug")]
    public async Task<ActionResult<object>> Debug(CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _mediator.Send(new GetEmployeesQuery(), cancellationToken);
            return Ok(new { 
                message = "Backend is working!", 
                employeeCount = employees.Count,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, stackTrace = ex.StackTrace });
        }
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
    public async Task<ActionResult<GetEmployee>> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateEmployeeCommand(request),cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateEmployeeRequest request,
        [FromServices] IEmployeeService employeeService,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        if (request.Id != id)
            return BadRequest("ID mismatch");
            
        var employee = await employeeService.GetById(id);
        if (employee == null)
            return NotFound("Employee not found");
            
        try
        {
            // Use the service method which handles validation
            var updatedEmployee = await employeeService.Update(request);
            var getEmployee = mapper.Map<GetEmployee>(updatedEmployee);
            return Ok(getEmployee);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while updating the employee" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        string id,
        [FromServices] IEmployeeService employeeService,
        CancellationToken cancellationToken)
    {
        var employee = await employeeService.GetById(id);
        if (employee == null)
            return NotFound();
        
        await employeeService.Delete(id);
        return NoContent();
    }

    [HttpGet("serials/{userName}")]
    public async Task<ActionResult<IEnumerable<GetAssetSerials>>> GetEmployeeAssetSerials(string userName, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetEmployeeAssetSerialsQuery(userName), cancellationToken));
    }
}