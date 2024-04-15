using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Common.Security;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Queries
{
    public class GetMyAssetTypesQuery : IRequest<IReadOnlyCollection<GetAssetTypeSerialDto>>;

    public class GetMyAssetsQueryHandler : IRequestHandler<GetMyAssetTypesQuery, IReadOnlyCollection<GetAssetTypeSerialDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyAssetsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyCollection<GetAssetTypeSerialDto>> Handle(GetMyAssetTypesQuery request, CancellationToken cancellationToken)
        {
            var currentUserName = _currentUserService.GetUserName();

            var allAssetTypes = await _context.AssetTypes.GetAllAsync(cancellationToken);
            var allUserAssetSerials = await _context.AssetSerials.GetAllAsync(cancellationToken);
            var userAssets = allUserAssetSerials.Where(a => a.Employee?.UserName == currentUserName).GroupBy(a => a.Asset.AssetTypeId);

            var result = new List<GetAssetTypeSerialDto>();
            foreach (var userAssetGroup in userAssets)
            {
                var assetType = allAssetTypes.FirstOrDefault(a => a.Id == userAssetGroup.Key);
                if (assetType is null) continue;

                var assetTypeSerial = new GetAssetTypeSerialDto
                {
                    Name = assetType.Name,
                    Icon = assetType.Icon
                };

                assetTypeSerial.AssetSerialsDto = new();
                foreach (var userAsset in userAssetGroup)
                {
                    assetTypeSerial.AssetSerialsDto.Add(new GetAssetSerialsDto
                    {
                        Name = userAsset.Asset.Name,
                        Serial = userAsset.Serial,
                        Id = userAsset.Id
                    });
                }

                result.Add(assetTypeSerial);
            }

            return _mapper.Map<IReadOnlyCollection<GetAssetTypeSerialDto>>(result);

        }
    }
}
