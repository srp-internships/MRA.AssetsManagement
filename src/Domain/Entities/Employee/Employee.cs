using System.Text.Json.Serialization;
using MRA.AssetsManagement.Domain.Common;
public class Employee : IEntity
{
    public string Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    
    [JsonIgnore]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    
}