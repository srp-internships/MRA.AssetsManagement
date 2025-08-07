namespace MRA.AssetsManagement.Web.Shared.Employees
{
    public class GetEmployee
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool CanLogin { get; set; } = true;
    }
}