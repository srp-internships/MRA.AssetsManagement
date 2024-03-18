using AutoMapper;

using MediatR;

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

    public CreateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = _mapper.Map<Tag>(request);

        await _context.Tags.CreateAsync(cancellationToken, tag);

        return tag;
    }
}