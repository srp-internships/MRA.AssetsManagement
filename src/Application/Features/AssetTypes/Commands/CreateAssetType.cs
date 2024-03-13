using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Commands;

public record CreateAssetTypeCommand(AssetType Type) : IRequest;

public class CreateAssetTypeCommandHandler  : IRequestHandler<CreateAssetTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateAssetTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(CreateAssetTypeCommand request, CancellationToken cancellationToken)
    {
       await _context.AssetTypes.CreateAsync(request.Type);
    }
}