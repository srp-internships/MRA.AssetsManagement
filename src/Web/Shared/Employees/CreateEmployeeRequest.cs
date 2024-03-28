using FluentValidation;

public class CreateEmployeeRequest
{
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public bool IsPhoneandEmailVerificationRequired { get; set; } = false;
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
public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeValidator()
    {
        RuleFor(s => s.Email).EmailAddress();
        RuleFor(s=>s.Username).NotEmpty();
        RuleFor(s=>s.FirstName).NotEmpty();
        RuleFor(s=>s.LastName).NotEmpty();
        RuleFor(s => s.Username).NotEmpty();
        
        RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
        RuleFor(s => s.ConfirmPassword).Equal(s => s.Password)
            .WithMessage("Passwords do not match.");
    }
}