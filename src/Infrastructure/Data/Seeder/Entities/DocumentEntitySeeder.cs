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
            new Document
            {
                Approved = true,
                Date = DateTime.Now.AddDays(-1),
                Details =
                [
                    new() { Price = 30000, Quantity = 3, Asset = assets.ElementAt(2) },
                    new() { Price = 14000, Quantity = 2, Asset = assets.ElementAt(1) },
                    new() { Price = 20000, Quantity = 2, Asset = assets.ElementAt(2) }
                ]
            }
        ]);
    }
}