using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Application.Features.Assets.Commands
{
    public record CreateAssetCommand(CreateAssetRequest Asset) : IRequest<GetAsset>;

    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, GetAsset>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAssetCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAsset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = _mapper.Map<Asset>(request.Asset);
            await _context.Assets.CreateAsync(cancellationToken, asset);
            return _mapper.Map<GetAsset>(asset);
        }

    }
}
