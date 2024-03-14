using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public class CreateAssetTypeCommand : IRequest<AssetType>
{
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string Icon { get; set; } = null!;
}

public class CreateAssetTypeCommandHandler : IRequestHandler<CreateAssetTypeCommand, AssetType>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateAssetTypeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AssetType> Handle(CreateAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var assetType = _mapper.Map<AssetType>(request);

        await _context.AssetTypes.CreateAsync(assetType);
        return assetType;
    }
}