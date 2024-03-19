using MediatR;

using MRA.AssetsManagement.Application.Data;

namespace MRA.AssetsManagement.Application.Features.Tags.Commands;

public record DeleteTagCommand(string Id) : IRequest<bool>;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.GetAsync(request.Id, cancellationToken);

        if (tag is null) 
            return false;
        
        await _context.Tags.RemoveAsync(request.Id, cancellationToken);

        return true;
    }
}