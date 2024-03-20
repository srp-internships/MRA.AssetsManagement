using MediatR;

namespace MRA.AssetsManagement.Domain.Entities;

public class RegisterEmployee : IRequest<Guid>
{
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public bool IsPhoneVerificationRequired { get; set; } = false;
    public bool IsEmailVerificationRequired { get; set; } = false;
    public int VerificationCode { get; set; }
    
    private string _username;
    public string Username
    {
        get { return _username; }
        set { _username = value.Trim(); }
    }
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
}