﻿using FluentValidation;

namespace MRA.AssetsManagement.Domain.Entities;

public class RegisterEmployeeValidator : AbstractValidator<RegisterEmployee>
{
    public RegisterEmployeeValidator()
    {
        RuleFor(s => s.Email).EmailAddress();
        RuleFor(s=>s.Username).NotEmpty();
        RuleFor(s=>s.FirstName).NotEmpty();
        RuleFor(s=>s.LastName).NotEmpty();
        RuleFor(s => s.PhoneNumber).Matches(@"^(?:\d{9}|\+992\d{9}|992\d{9})$")
            .WithMessage("Invalid phone number. Example : +992921234567, 992921234567, 921234567");
        RuleFor(s => s.Username.Trim());
        RuleFor(s => s.Username).MinimumLength(6)
            .Must(username => username.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '@'))
            .WithMessage("Username should only consist of letters, numbers, underscores, or the @ symbol.");
        
        RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
        RuleFor(s => s.ConfirmPassword).Equal(s => s.Password)
            .WithMessage("Passwords do not match.");
    }
}