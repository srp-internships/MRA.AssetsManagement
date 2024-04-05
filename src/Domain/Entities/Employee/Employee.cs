using System.Text.Json.Serialization;
using MRA.AssetsManagement.Domain.Common;

namespace MRA.AssetsManagement.Domain.Entities.Employee;

public class Employee : IEntity
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;

    [JsonIgnore] public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

}