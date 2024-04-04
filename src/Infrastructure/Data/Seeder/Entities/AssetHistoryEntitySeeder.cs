using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Domain.Enums;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class AssetHistoryEntitySeeder : EntitySeeder<AssetHistory>
{
    private readonly IApplicationDbContext _context;

    public AssetHistoryEntitySeeder(IApplicationDbContext context) : base(context.AssetHistories)
    {
        _context = context;
    }

    public override async Task Development()
    {
        if (await _repository.AnyAsync()) return;

        var pc1 = await _context.AssetSerials.GetAsync(x => x.Id == "660aab4f7e86b88cd4bbacc8");
        var pc2 = await _context.AssetSerials.GetAsync(x => x.Id == "660aab797e86b88cd4bbacc9");
        var pc3 = await _context.AssetSerials.GetAsync(x => x.Id == "660aab937e86b88cd4bbacca");

        var abbos = new UserDisplay() { FirstName = "Abbos", LastName = "Sidiqov", Username = "abbosidiqov" };
        var nizomjon = new UserDisplay() { FirstName = "Nizomjon", LastName = "Rahmonberdiev", Username = "nizomjon" };
        var shuhrat = new UserDisplay() { FirstName = "Shuhrat", LastName = "Rahmonov", Username = "shuhrat" };
        AssetHistory[] histories = [
            // PC-000001
            new AssetHistory
            {
                AssetSerial = pc1,
                DateTime = DateTime.Now.AddMonths(-2),
            },
            new AssetHistory
            {
                AssetSerial = pc1 with {Status = AssetStatus.Taken, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(5),
                UserId = "nizomjon"
            }
        ];

        

        await _repository.CreateAsync(default, histories.ToArray());
    }
}