using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Common.Exceptions;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public class UpdateAssetTypeCommand : IRequest
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public bool Archived { get; set; }
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
            throw new NotFoundEntityException(nameof(AssetType), request.Id);

        _mapper.Map(request, assetType);

        await _context.AssetTypes.UpdateAsync(assetType, cancellationToken);
    }
}