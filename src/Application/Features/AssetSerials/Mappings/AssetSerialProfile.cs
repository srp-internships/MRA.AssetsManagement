using AutoMapper;

using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.AssetSerials.Mappings;

public class AssetSerialProfile : Profile
{
    public AssetSerialProfile()
    {
        CreateMap<UserDisplay, MRA.AssetsManagement.Web.Shared.Employees.UserDisplay>().ForMember(dest => dest.FullName, opt => opt.Ignore());
        CreateMap<MRA.AssetsManagement.Web.Shared.Employees.UserDisplay, UserDisplay>();
        CreateMap<AssetSerial, HistoryAssetSerial>();
    }
}