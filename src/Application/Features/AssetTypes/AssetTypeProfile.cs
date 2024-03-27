using AutoMapper;

using MRA.AssetsManagement.Application.Features.AssetTypes.Commands;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Application.Features.AssetTypes;

public class AssetTypeProfile : Profile
{
    public AssetTypeProfile()
    {
        CreateMap<CreateAssetTypeRequest, AssetType>();
        CreateMap<UpdateAssetTypeCommand, AssetType>();
        CreateMap<AssetType, GetAssetType>();
    }
}