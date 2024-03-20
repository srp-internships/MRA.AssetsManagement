using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Tags.Queries;

public record GetTagsQuery : IRequest<IReadOnlyCollection<Tag>>;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IReadOnlyCollection<Tag>>
{
    private readonly IApplicationDbContext _context;

    public GetTagsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyCollection<Tag>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags.GetAllAsync(cancellationToken);
    }
}
