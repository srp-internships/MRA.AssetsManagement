using AutoMapper;
using Microsoft.Extensions.Configuration;
using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Application.Common.Services.PasswordService;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Infrastructure.Identity.Services;

public class EmployeeService(
    IApplicationDbContext context,
    IPasswordService passwordService,
    IMapper mapper)
    : IEmployeeService
{
    public async Task<List<Employee>> GetAll()
    {
        var employees = await context.Employees.GetAllAsync();
        return employees.ToList();
    }

    public async Task<Employee?> GetByUserName(string userName)
    {
        var employee = await context.Employees.GetAsync(e => e.UserName == userName, CancellationToken.None);
        return employee;
    }

    public async Task<Employee?> GetByEmail(string email)
    {
        var employee = await context.Employees.GetAsync(e => e.Email == email, CancellationToken.None);
        return employee;
    }

    public async Task<Employee?> GetById(string id)
    {
        return await context.Employees.GetAsync(e => e.Id == id, CancellationToken.None);
    }

    public async Task<string> Create(CreateEmployeeRequest createEmployeeRequest)
    {
        // Check if username already exists
        var existingUser = await context.Employees.GetAsync(e => e.UserName == createEmployeeRequest.Username, CancellationToken.None);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        // Check if email already exists
        var existingEmail = await context.Employees.GetAsync(e => e.Email == createEmployeeRequest.Email, CancellationToken.None);
        if (existingEmail != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        // Hash password
        var hashedPassword = passwordService.HashPassword(createEmployeeRequest.Password);

        // Create new employee
        var employee = new Employee
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createEmployeeRequest.Username,
            Email = createEmployeeRequest.Email,
            Password = hashedPassword,
            FirstName = createEmployeeRequest.FirstName,
            LastName = createEmployeeRequest.LastName,
            PhoneNumber = createEmployeeRequest.PhoneNumber,
            CanLogin = createEmployeeRequest.CanLogin
        };

        // Save to database
        await context.Employees.CreateAsync(CancellationToken.None, employee);

        return employee.Id;
    }

    public async Task<Employee> Update(MRA.AssetsManagement.Web.Shared.Employees.UpdateEmployeeRequest updatedEmployee)
    {
        var employee = await context.Employees.GetAsync(e => e.Id == updatedEmployee.Id, CancellationToken.None);
        if (employee == null)
            throw new InvalidOperationException("Employee not found");
        
        // Check if username is being changed and if it already exists
        if (employee.UserName != updatedEmployee.UserName)
        {
            var existingUser = await context.Employees.GetAsync(e => e.UserName == updatedEmployee.UserName, CancellationToken.None);
            if (existingUser != null)
                throw new InvalidOperationException("Username already exists");
        }
        
        // Check if email is being changed and if it already exists
        if (employee.Email != updatedEmployee.Email)
        {
            var existingEmail = await context.Employees.GetAsync(e => e.Email == updatedEmployee.Email, CancellationToken.None);
            if (existingEmail != null)
                throw new InvalidOperationException("Email already exists");
        }
        
        // Update all fields
        employee.FirstName = updatedEmployee.FirstName;
        employee.LastName = updatedEmployee.LastName;
        employee.UserName = updatedEmployee.UserName;
        employee.Email = updatedEmployee.Email;
        employee.PhoneNumber = updatedEmployee.PhoneNumber;
        employee.CanLogin = updatedEmployee.CanLogin;
        
        await context.Employees.UpdateAsync(employee);
        return employee;
    }

    public async Task<Employee> Update(MRA.AssetsManagement.Web.Shared.Employees.GetEmployee updatedEmployee)
    {
        // Map GetEmployee to UpdateEmployeeRequest
        var updateRequest = new MRA.AssetsManagement.Web.Shared.Employees.UpdateEmployeeRequest
        {
            Id = updatedEmployee.Id,
            FirstName = updatedEmployee.FirstName,
            LastName = updatedEmployee.LastName,
            UserName = updatedEmployee.UserName,
            Email = updatedEmployee.Email,
            PhoneNumber = updatedEmployee.PhoneNumber,
            CanLogin = updatedEmployee.CanLogin
        };
        return await Update(updateRequest);
    }

    public async Task Update(Employee employee)
    {
        await context.Employees.UpdateAsync(employee);
    }

    public async Task Delete(string employeeId)
    {
        var employee = await context.Employees.GetAsync(e => e.Id == employeeId, CancellationToken.None);
        if (employee == null)
            throw new InvalidOperationException("Employee not found");
        
        await context.Employees.RemoveAsync(employeeId, CancellationToken.None);
    }
}