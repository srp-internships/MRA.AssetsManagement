using AutoMapper;
using MRA.AssetsManagement.Application.Features.Documents.Create;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Documents.Mappings;

public class DocumentProfile : Profile
{
    public DocumentProfile()
    {
        CreateMap<CreateDocumentDetailCommand, DocumentDetail>()
            .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Asset.Id));
    }
}