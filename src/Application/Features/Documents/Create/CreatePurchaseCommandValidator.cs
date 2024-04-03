using FluentValidation;

namespace MRA.AssetsManagement.Application.Features.Documents.Create;

public class CreatePurchaseCommandValidator : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseCommandValidator()
    {
        RuleFor(x => x.Vendor)
            .NotNull().NotEmpty()
            .WithMessage("'Vendor' must not be NULL or EMPTY");
    }
}

public class DocumentDetailValidator : AbstractValidator<CreateDocumentDetailCommand>
{
    public DocumentDetailValidator()
    {
        RuleFor(x => x.Price).Must(x => x >= 0);
        RuleFor(x => x.Quantity).Must(x => x >= 0);
    }
}