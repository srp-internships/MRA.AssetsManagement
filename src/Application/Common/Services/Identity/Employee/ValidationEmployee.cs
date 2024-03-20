using FluentValidation;
using MRA.AssetsManagement.Domain.Entities;


public class ValidationEmployee : AbstractValidator<Employee>
{
    public ValidationEmployee()
    {
        RuleFor(e => e.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
        RuleFor(e => e.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
        RuleFor(e => e.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email is required");
        RuleFor(e => e.Password).NotNull().NotEmpty().MinimumLength(6)
            .WithMessage("Password is required and minimum length is 6");
        
    }
}