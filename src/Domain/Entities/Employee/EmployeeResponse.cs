namespace MRA.AssetsManagement.Domain.Entities;

public class EmployeeResponse
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public bool EmailConfirmed { get; init; }
    public bool PhoneNumberConfirmed { get; init; }
    public string FullName { get; init; }
    public DateTime DateOfBirth { get; init; }
}