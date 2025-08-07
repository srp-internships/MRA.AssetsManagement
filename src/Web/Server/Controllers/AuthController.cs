using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Features.Auth.Commands;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MRA.AssetsManagement.Application.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MRA.AssetsManagement.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("debug")]
    [Authorize]
    public ActionResult<object> DebugAuth()
    {
        var user = User;
        var claims = user.Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
        
        return Ok(new
        {
            IsAuthenticated = user.Identity?.IsAuthenticated ?? false,
            Username = user.Identity?.Name,
            Claims = claims
        });
    }

    [HttpGet("debug-admin")]
    public async Task<ActionResult<object>> DebugAdminUser()
    {
        try
        {
            // Get the database context to check admin user
            var dbContext = HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();
            
            // Check if admin user exists
            var adminUser = await dbContext.Employees.GetAsync(e => e.UserName == "admin", CancellationToken.None);
            
            if (adminUser == null)
            {
                return Ok(new
                {
                    Message = "Admin user not found in database",
                    Recommendation = "Restart application to trigger seeding, or manually create admin user"
                });
            }
            
            return Ok(new
            {
                Message = "Admin user found in database",
                User = new
                {
                    Id = adminUser.Id,
                    UserName = adminUser.UserName,
                    Email = adminUser.Email,
                    FirstName = adminUser.FirstName,
                    LastName = adminUser.LastName
                },
                Recommendation = "Admin user is ready for authentication"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }

    [HttpGet("decode-token")]
    public ActionResult<object> DecodeToken([FromQuery] string token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token parameter is required" });
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            
            if (jsonToken == null)
            {
                return BadRequest(new { message = "Invalid JWT token" });
            }

            var claims = jsonToken.Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
            
            return Ok(new
            {
                Header = jsonToken.Header,
                Payload = jsonToken.Payload,
                Claims = claims,
                Issuer = jsonToken.Issuer,
                Audience = jsonToken.Audiences?.ToList(),
                ValidFrom = jsonToken.ValidFrom,
                ValidTo = jsonToken.ValidTo
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 