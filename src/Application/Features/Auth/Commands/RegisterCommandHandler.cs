using MediatR;
using MRA.AssetsManagement.Application.Common.Services.PasswordService;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public RegisterCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if username already exists
        var existingUser = await _context.Employees.GetAsync(e => e.UserName == request.Username, cancellationToken);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        // Check if email already exists
        var existingEmail = await _context.Employees.GetAsync(e => e.Email == request.Email, cancellationToken);
        if (existingEmail != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        // Hash password
        var hashedPassword = _passwordService.HashPassword(request.Password);

        // Create new employee
        var employee = new Employee
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.Username,
            Email = request.Email,
            Password = hashedPassword,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };

        // Save to database
        await _context.Employees.CreateAsync(cancellationToken, employee);

        return new RegisterResponse
        {
            UserId = employee.Id,
            Username = employee.UserName,
            Email = employee.Email,
            Message = "User registered successfully"
        };
    }
} 