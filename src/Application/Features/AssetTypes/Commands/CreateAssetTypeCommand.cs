using AutoMapper;

using FluentValidation;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public record CreateAssetTypeCommand(CreateAssetTypeRequest AssetType) : IRequest<GetAssetType>;

public class CreateAssetTypeCommandValidator : AbstractValidator<CreateAssetTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateAssetTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(p => p.AssetType).SetValidator(new CreateAssetTypeRequestValidator());
        
        // Extended validation for server-side.
        RuleFor(p => p.AssetType.Name)
            .MustAsync(BeUniqueName)
            .WithMessage("'Name' must be unique.")
            .WithErrorCode("UNIQUE_NAME");
        
        RuleFor(p => p.AssetType.ShortName)
            .MustAsync(BeUniqueShortName)
            .WithMessage("'ShortName' must be unique.")
            .WithErrorCode("UNIQUE_SHORTNAME");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.AssetTypes
            .AnyAsync(x => x.Name == name, cancellationToken);
    }
    
    public async Task<bool> BeUniqueShortName(string shortName, CancellationToken cancellationToken)
    {
        return await _context.AssetTypes
            .AnyAsync(x => x.ShortName == shortName, cancellationToken);
    }
}

public class CreateAssetTypeCommandHandler : IRequestHandler<CreateAssetTypeCommand, GetAssetType>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateAssetTypeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAssetType> Handle(CreateAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var assetType = _mapper.Map<AssetType>(request.AssetType);

        await _context.AssetTypes.CreateAsync(cancellationToken, assetType);
        return _mapper.Map<GetAssetType>(assetType);
    }
}