using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;
using MRA.AssetsManagement.Domain.Enums;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class AssetSerialEntitySeeder : EntitySeeder<AssetSerial>
{
    private readonly IApplicationDbContext _context;

    public AssetSerialEntitySeeder(IApplicationDbContext context) : base(context.AssetSerials)
    {
        _context = context;
    }

    public override async Task Development()
    {
        if (await _repository.AnyAsync()) return;

        var assets = await _context.Assets.GetAllAsync();
        var abbos = new UserDisplay { FirstName = "Abbos", LastName = "Sidiqov", UserName = "abbosidiqov" };
        var nizomjon = new UserDisplay { FirstName = "Nizomjon", LastName = "Rahmonberdiev", UserName = "nizomjon" };
        var shuhrat = new UserDisplay { FirstName = "Shuhrat", LastName = "Rahmonov", UserName = "shuhrat" };

        await _repository.CreateAsync(default, [
            new AssetSerial
            {
                Id = "660aab4f7e86b88cd4bbacc8",
                Status = AssetStatus.Broken,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062261"),
                Serial = "PC-000001",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2).AddDays(50),
                LastModifiedBy = nizomjon.UserName,
                Employee = abbos
            },
            new AssetSerial
            {
                Id = "660aab797e86b88cd4bbacc9",
                Status = AssetStatus.Taken,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062261"),
                Serial = "PC-000002",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2).AddDays(7),
                LastModifiedBy = nizomjon.UserName,
                Employee = shuhrat
            },
            new AssetSerial
            {
                Id = "660aab937e86b88cd4bbacca",
                Status = AssetStatus.Available,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062261"),
                Serial = "PC-000003",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2),
                LastModifiedBy = nizomjon.UserName,
                Employee = abbos
            },
            new AssetSerial
            {
                Id = "660aabba7e86b88cd4bbaccc",
                Status = AssetStatus.Taken,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062260"),
                Serial = "LPT-000001",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2).AddDays(11),
                LastModifiedBy = nizomjon.UserName,
                Employee = shuhrat
            },
            new AssetSerial
            {
                Id = "660aabcf7e86b88cd4bbaccd",
                Status = AssetStatus.Broken,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062260"),
                Serial = "LPT-000002",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2).AddDays(33),
                LastModifiedBy = nizomjon.UserName,
                Employee = abbos
            },
            new AssetSerial
            {
                Id = "660aabe07e86b88cd4bbacce",
                Status = AssetStatus.Taken,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062266"),
                Serial = "MTR-000001",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2).AddDays(20),
                LastModifiedBy = nizomjon.UserName,
                Employee = abbos
            },
            new AssetSerial
            {
                Id = "660aabf27e86b88cd4bbaccf",
                Status = AssetStatus.Available,
                Asset = assets.First(x => x.Id == "660510ad00baa1f0e9062266"),
                Serial = "MTR-000002",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2),
                LastModifiedBy = nizomjon.UserName,
                Employee = shuhrat
            },
            new AssetSerial
            {
                Id = "660aac267e86b88cd4bbacd2",
                Status = AssetStatus.Deprecated,
                Asset = assets.First(x => x.Id == "6605130500baa1f0e906232a"),
                Serial = "CHR-000001",
                CreatedAt = DateTime.Now.AddMonths(-2),
                CreatedBy = nizomjon.UserName,
                LastModifiedAt = DateTime.Now.AddMonths(-2).AddDays(57),
                LastModifiedBy = nizomjon.UserName,
                Employee = shuhrat
            }
        ]);
    }
}