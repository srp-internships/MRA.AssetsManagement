﻿using FluentValidation;

namespace MRA.AssetsManagement.Web.Shared.Employees;

public class CreateEmployeeRequest
{
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    private string _username = null!;
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
        RuleFor(s => s.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("'Confirm password' must be equal to the 'Password' field!!");
    }
}