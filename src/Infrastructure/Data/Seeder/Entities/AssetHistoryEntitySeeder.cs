using MRA.AssetsManagement.Application.Data;
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

        var lpt1 = await _context.AssetSerials.GetAsync("660aabba7e86b88cd4bbaccc");
        var lpt2 = await _context.AssetSerials.GetAsync("660aabcf7e86b88cd4bbaccd");

        var mtr1 = await _context.AssetSerials.GetAsync("660aabe07e86b88cd4bbacce");
        var mtr2 = await _context.AssetSerials.GetAsync("660aabf27e86b88cd4bbaccf");
        
        var chr1 = await _context.AssetSerials.GetAsync("660aac267e86b88cd4bbacd2");

        var abbos = new UserDisplay { FirstName = "Abbos", LastName = "Sidiqov", UserName = "abbosidiqov" };
        var nizomjon = new UserDisplay { FirstName = "Nizomjon", LastName = "Rahmonberdiev", UserName = "nizomjon" };
        var shuhrat = new UserDisplay { FirstName = "Shuhrat", LastName = "Rahmonov", UserName = "shuhrat" };
        
        AssetHistory[] histories = [
            // PC-000001
            new AssetHistory
            {
                AssetSerial = pc1 with {Status = AssetStatus.Available, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = pc1 with {Status = AssetStatus.Taken, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(5),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = pc1 with {Status = AssetStatus.Broken, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(50),
                UserId = nizomjon.UserName
            },
            // PC-000002
            new AssetHistory
            {
                AssetSerial = pc2 with {Status = AssetStatus.Available, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = pc2 with {Status = AssetStatus.Taken, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(7),
                UserId = nizomjon.UserName
            },
            //PC-000003
            new AssetHistory
            {
                AssetSerial = pc3 with {Status = AssetStatus.Available, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            //LPT-000001
            new AssetHistory
            {
                AssetSerial = lpt1 with {Status = AssetStatus.Available, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = lpt1 with {Status = AssetStatus.Taken, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(11),
                UserId = nizomjon.UserName
            },
            //LPT-000002
            new AssetHistory
            {
                AssetSerial = lpt2 with {Status = AssetStatus.Available, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = lpt2 with {Status = AssetStatus.Taken, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(20),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = lpt2 with {Status = AssetStatus.Broken, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(33),
                UserId = nizomjon.UserName
            },
            // MTR-000001
            new AssetHistory
            {
                AssetSerial = mtr1 with {Status = AssetStatus.Available, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = mtr1 with {Status = AssetStatus.Taken, Employee = abbos },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(20),
                UserId = nizomjon.UserName
            },
            // MTR-000002
            new AssetHistory
            {
                AssetSerial = mtr2 with {Status = AssetStatus.Available, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            // CHR-000001
            new AssetHistory
            {
                AssetSerial = chr1 with {Status = AssetStatus.Available, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = chr1 with {Status = AssetStatus.Taken, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(23),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = chr1 with {Status = AssetStatus.Broken, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(50),
                UserId = nizomjon.UserName
            },
            new AssetHistory
            {
                AssetSerial = chr1 with {Status = AssetStatus.Deprecated, Employee = shuhrat },
                DateTime = DateTime.Now.AddMonths(-2).AddDays(57),
                UserId = nizomjon.UserName
            }
        ];

        await _repository.CreateAsync(default, histories.ToArray());
    }
}