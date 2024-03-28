namespace MRA.AssetsManagement.Application.Features.Employees;

public class EmployeeResponse
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public bool EmailConfirmed { get; init; }
    public bool PhoneNumberConfirmed { get; init; }
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
}