using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Application.Data;

namespace MRA.AssetsManagement.Application.Features.Tags.Commands;

public class UpdateTagCommand : IRequest
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Color { get; set; }
}

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.GetAsync(request.Id, cancellationToken);
        
        if (tag is null)
            throw new Exception("Tag with provided Id was not found.");
        
        _mapper.Map(request, tag);
        
        await _context.Tags.UpdateAsync(tag, cancellationToken);
    }
}