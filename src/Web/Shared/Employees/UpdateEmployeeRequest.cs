namespace MRA.AssetsManagement.Web.Shared.Employees;

public class UpdateEmployeeRequest
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public bool CanLogin { get; set; } = true;
}