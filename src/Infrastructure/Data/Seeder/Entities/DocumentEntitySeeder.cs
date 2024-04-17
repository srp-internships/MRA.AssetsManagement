using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class DocumentEntitySeeder : EntitySeeder<Document>
{
    private readonly IApplicationDbContext _context;

    public DocumentEntitySeeder(IApplicationDbContext context) : base(context.Documents)
    {
        _context = context;
    }

    public async override Task Development()
    {
        if (await _repository.AnyAsync()) return;

        var asusPc = await _context.Assets.GetAsync("660510ad00baa1f0e9062261");
        var lgMonitor = await _context.Assets.GetAsync("660510ad00baa1f0e9062266");
        var dellLaptop = await _context.Assets.GetAsync("660510ad00baa1f0e9062260");
        var chair = await _context.Assets.GetAsync("6605130500baa1f0e906232a");
            
        await _repository.CreateAsync(default, [
            new PurchaseDocument
            {
                Approved = true,
                Date = DateTime.Now.AddMonths(-2),
                Details =
                [
                    new() { Id = asusPc.Id, Price = 30000, Quantity = 3, Asset = asusPc },
                    new() { Id = lgMonitor.Id, Price = 14000, Quantity = 2, Asset = lgMonitor },
                    new() { Id = dellLaptop.Id, Price = 20000, Quantity = 2, Asset = dellLaptop },
                    new() { Id = chair.Id, Price = 1500, Quantity = 1, Asset = chair }
                ],
                Vendor = "Tech Mark"
            }
        ]);
    }
}