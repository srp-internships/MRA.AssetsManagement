using MediatR;
using MRA.AssetsManagement.Application.Common.Services.JwtService;
using MRA.AssetsManagement.Application.Common.Services.PasswordService;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService,
        IJwtService jwtService)
    {
        _context = context;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Find user by username or email
        var employee = await _context.Employees.GetAsync(e => 
            e.UserName == request.Username || e.Email == request.Username, cancellationToken);

        if (employee == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        if (!employee.CanLogin)
        {
            throw new UnauthorizedAccessException("Login is disabled for this user");
        }

        // Verify password
        if (!_passwordService.VerifyPassword(request.Password, employee.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        // Generate tokens
        var accessToken = _jwtService.GenerateAccessToken(
            employee.Id, 
            employee.UserName, 
            employee.Email);

        var refreshToken = _jwtService.GenerateRefreshToken();

        var expiresAt = DateTime.UtcNow.AddDays(1);
        
        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            Username = employee.UserName,
            Email = employee.Email
        };
    }
} 