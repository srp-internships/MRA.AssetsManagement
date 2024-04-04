using AutoMapper;
using MRA.AssetsManagement.Application.Features.Tags.Commands;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Application.Features.Tags.Mappings;

public class TagMappings : Profile
{
    public TagMappings()
    {
        CreateMap<CreateTagCommand, Tag>();
        CreateMap<UpdateTagCommand, Tag>();
        CreateMap<Tag, GetTag>();
    }
}