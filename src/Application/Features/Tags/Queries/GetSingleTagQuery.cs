using AutoMapper;
using MediatR;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Application.Features.Tags.Queries
{
    public record GetSingleTagQuery(string Slug) : IRequest<GetTag>;

    public class GetSingleTagQueryHandler : IRequestHandler<GetSingleTagQuery, GetTag>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSingleTagQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetTag> Handle(GetSingleTagQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Tags.GetAsync(x => x.Slug == request.Slug, cancellationToken);
            return _mapper.Map<GetTag>(result);
        }
    }
}
