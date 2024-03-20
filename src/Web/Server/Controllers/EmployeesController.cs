using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        string pathJson = _configuration.GetValue<string>("SeeedEmployeePath");
        string jsonContent = System.IO.File.ReadAllText(pathJson);
        List<RegisterEmployee> registerEmployeeCommands = JsonConvert.DeserializeObject<List<RegisterEmployee>>(jsonContent);
       
        var response = await _mediator.Send(new SeedEmployeeCommand(registerEmployeeCommands));
        return Ok(response);
    }

    [HttpGet("getAll")]
    public async Task<List<EmployeeResponse>> GetAll()
    {
        var token = HttpContext.Request.Headers.Authorization.ToString();
        var response = await _mediator.Send(new GetEmployeesQuery(token));
        return response;
    }
    
    [HttpGet("getById")]
    [Authorize]
    public async Task<EmployeeResponse> GetById(string id)
    {
        var token = HttpContext.Request.Headers.Authorization.ToString();
        var response = await _mediator.Send(new GetEmployeeByIdQuery(id,token));
        return response;
    }
    
    [HttpPost("create")]
    public async Task<string> Create(RegisterEmployee registerEmployee)
    {
        var token = HttpContext.Request.Headers.Authorization.ToString();
        return await _mediator.Send(new CreateEmployeeCommand(registerEmployee,token));
    }
}