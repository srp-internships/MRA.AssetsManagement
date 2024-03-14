using FluentValidation;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public class UpdateAssetTypeCommandValidator : AbstractValidator<UpdateAssetTypeCommand>
{
    public UpdateAssetTypeCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(2, 128);
        RuleFor(x => x.ShortName).NotNull().NotEmpty().Length(1, 6);
    }
}