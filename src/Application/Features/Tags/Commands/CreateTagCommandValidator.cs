using FluentValidation;

namespace MRA.AssetsManagement.Application.Features.Tags.Commands;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}