using MediatR;

using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Web.Shared.AssetSerialHistory;
using MRA.AssetsManagement.Web.Shared.Enums;

namespace MRA.AssetsManagement.Application.Features.AssetHistories.Queries;

public class GetAssetHistoryForSerialQuery(string serial) : IRequest<IEnumerable<GetAssetSerialHistory>>
{
    public string Serial { get; set; } = serial;
}

public class GetAssetHistoryForSerialQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAssetHistoryForSerialQuery, IEnumerable<GetAssetSerialHistory>>
{
    public async Task<IEnumerable<GetAssetSerialHistory>> Handle(GetAssetHistoryForSerialQuery request, CancellationToken cancellationToken)
    {
        var histories = await context.AssetHistories.GetAllAsync(x => x.HistoryAssetSerial.Serial == request.Serial);

        return histories.OrderByDescending(x => x.DateTime).Select(x => new GetAssetSerialHistory
        {
            Employee = x.HistoryAssetSerial.Employee?.FullName,
            Date = DateOnly.FromDateTime(x.DateTime),
            Status = Enum.Parse<AssetStatus>(x.HistoryAssetSerial.Status.ToString()),
            Note = x.Note
        });
    }
}