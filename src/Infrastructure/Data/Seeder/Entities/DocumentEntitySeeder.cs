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
        if (await _repository.Any()) return;

        var assets = await _context.Assets.GetAllAsync();

        await _repository.CreateAsync(default, [
            new PurchaseDocument
            {
                Approved = true,
                Date = DateTime.Now.AddMonths(-2),
                Details =
                [
                    new() { Id = assets.ElementAt(2).Id, Price = 30000, Quantity = 3, Asset = assets.ElementAt(2) },
                    new() { Id = assets.ElementAt(1).Id, Price = 14000, Quantity = 2, Asset = assets.ElementAt(1) },
                    new() { Id = assets.ElementAt(0).Id, Price = 20000, Quantity = 2, Asset = assets.ElementAt(0) },
                    new() { Id = assets.ElementAt(3).Id, Price = 1500, Quantity = 1, Asset = assets.ElementAt(3) }
                ],
                Vendor = "Some Vendor"
            }
        ]);
    }
}