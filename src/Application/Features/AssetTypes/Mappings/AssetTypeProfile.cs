using AutoMapper;
using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Mappings;

public class AssetTypeProfile : Profile
{
    public AssetTypeProfile()
    {
        CreateMap<CreateAssetTypeCommand, AssetType>();
        CreateMap<UpdateAssetTypeCommand, AssetType>();
    }
}