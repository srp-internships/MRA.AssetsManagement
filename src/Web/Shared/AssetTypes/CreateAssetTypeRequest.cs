using FluentValidation;

namespace MRA.AssetsManagement.Web.Shared.AssetTypes;

public class CreateAssetTypeRequest
{
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public List<Properties> Properties { get; set; } = [];
    public string Icon { get; set; } = null!;
}


public class CreateAssetTypeRequestValidator
    : AbstractValidator<CreateAssetTypeRequest>
{
    public CreateAssetTypeRequestValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(240)
            .NotEmpty();
        RuleFor(v => v.ShortName)
            .MaximumLength(6)
            .NotEmpty();
    }
}

public class PropertiesValidator : AbstractValidator<Properties>
{
    public PropertiesValidator()
    {
        RuleFor(x => x.Label).NotNull().NotEmpty();
        RuleFor(x => x.Value).NotNull().NotEmpty();
    }
}

