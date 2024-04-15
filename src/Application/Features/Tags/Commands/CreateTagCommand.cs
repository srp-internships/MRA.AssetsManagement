using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Common.Services.SlugGeneratorService;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Tags.Commands;

public class CreateTagCommand :IRequest<Tag>
{
    public string Name { get; set; } = null!;
    public string? Color { get; set; }
}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Tag>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISlugGeneratorService _slugGeneratorService;

    public CreateTagCommandHandler(IApplicationDbContext context, IMapper mapper, ISlugGeneratorService slugGeneratorService)
    {
        _context = context;
        _mapper = mapper;
        _slugGeneratorService = slugGeneratorService;
    }
    
    public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _mapper.Map<Tag>(request);
        tag.Slug = _slugGeneratorService.GenerateSlug(tag.Name);
        await _context.Tags.CreateAsync(cancellationToken, tag);

        return tag;
    }
}