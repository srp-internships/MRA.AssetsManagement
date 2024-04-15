using AutoMapper;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Application.Features.Assets.Mappings
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<CreateAssetRequest, Asset>();
            CreateMap<Asset, GetAsset>();
        }
    }
}
