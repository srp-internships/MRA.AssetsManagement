using FluentValidation;

namespace MRA.AssetsManagement.Application.Features.Tags.Commands;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}