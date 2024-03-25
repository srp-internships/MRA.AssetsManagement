using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MRA.AssetsManagement.Application.Features.Employees.Handlers;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Server.Controllers;
using Newtonsoft.Json;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public EmployeesController(IMediator mediator,IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }
    [HttpPost("seed")]
    public async Task<ActionResult<string>> SeedEmployeeData()
    {
        try
        {
            string pathJson = _configuration.GetValue<string>("SeeedEmployeePath");
            string jsonContent = System.IO.File.ReadAllText(pathJson);
            List<RegisterEmployee> registerEmployeeCommands = JsonConvert.DeserializeObject<List<RegisterEmployee>>(jsonContent);
            var response = await _mediator.Send(new SeedEmployeeCommand(registerEmployeeCommands));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<List<EmployeeResponse>>> GetAll()
    {
        try
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var response = await _mediator.Send(new GetEmployeesQuery(token));
            return response;
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("getById")]
    [Authorize]
    public async Task<ActionResult<EmployeeResponse>> GetById(string id)
    {
        try
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var response = await _mediator.Send(new GetEmployeeByIdQuery(id,token));
            return response;
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet("getByEmail")]
    [Authorize]
    public async Task<ActionResult<EmployeeResponse>> GetByEmail(string email)
    {
        try
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var response = await _mediator.Send(new GetEmployeeByEmailQuery(email,token));
            return response;
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<string>> Create(RegisterEmployee registerEmployee)
    {
        try
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return await _mediator.Send(new CreateEmployeeCommand(registerEmployee,token));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        
    }
}