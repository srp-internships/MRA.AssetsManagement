using FluentValidation;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Application.Features.Assets.Commands
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IApplicationDbContext _context;
            
        public CreateAssetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Asset.Name)
                .MustAsync(BeUniqueName)
                .WithMessage("'Name' must be unique.")
                .WithErrorCode("UNIQUE_NAME");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var result = await _context.Assets.AnyAsync(x => x.Name == name, cancellationToken);
            return !result;
        }
    }
}
