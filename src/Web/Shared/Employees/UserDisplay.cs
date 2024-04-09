namespace MRA.AssetsManagement.Web.Shared.Employees;

public class UserDisplay
{
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        return FullName;
    }
}