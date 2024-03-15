using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Data;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public class UpdateAssetTypeCommand : IRequest
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string Icon { get; set; } = null!;
}

public class UpdateAssetTypeCommandHandler : IRequestHandler<UpdateAssetTypeCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateAssetTypeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var assetType = await _context.AssetTypes.GetAsync(request.Id, cancellationToken);
        
        if (assetType is null)
            throw new Exception("AssetType with provided Id was not found.");
        
        _mapper.Map(request, assetType);

        await _context.AssetTypes.UpdateAsync(assetType, cancellationToken);
    }
}