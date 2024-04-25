using AutoMapper;

using FluentValidation;

using MediatR;

using MRA.AssetsManagement.Application.Common.Services.SlugGeneratorService;
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

        RuleForEach(x => x.AssetType.Properties).SetValidator(new PropertiesValidator());
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
       var result = await _context.AssetTypes
            .AnyAsync(x => x.Name == name, cancellationToken);
        return !result;
    }

    public async Task<bool> BeUniqueShortName(string shortName, CancellationToken cancellationToken)
    {
        var result = await _context.AssetTypes
             .AnyAsync(x => x.ShortName == shortName, cancellationToken);
        return !result;
    }
}

public class CreateAssetTypeCommandHandler : IRequestHandler<CreateAssetTypeCommand, GetAssetType>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISlugGeneratorService _slugGeneratorService;

    public CreateAssetTypeCommandHandler(IApplicationDbContext context, IMapper mapper, ISlugGeneratorService slugGeneratorService)
    {
        _context = context;
        _mapper = mapper;
        _slugGeneratorService = slugGeneratorService;
    }

    public async Task<GetAssetType> Handle(CreateAssetTypeCommand request ,CancellationToken cancellationToken)
    {
        var assetType = _mapper.Map<AssetType>(request.AssetType);
        assetType.Slug = _slugGeneratorService.GenerateSlug(assetType.Name);
        await _context.AssetTypes.CreateAsync(cancellationToken, assetType);
        return _mapper.Map<GetAssetType>(assetType);
    }
}