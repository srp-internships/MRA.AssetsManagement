using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class AssetEntitySeeder : EntitySeeder<Asset>
{
    private readonly IApplicationDbContext _context;

    public AssetEntitySeeder(IApplicationDbContext context) : base(context.Assets)
    {
        _context = context;
    }

    public override async Task Development()
    {
        if (await _repository.AnyAsync()) return;
     
        await _repository.CreateAsync(default, [
            // PC
            new Asset { Id = "660510ad00baa1f0e906225d", Name = "MSI CPU-i9 13300K RAM-32G SSD-1T", AssetTypeId = "6602b5508836f41c710e02ed" },
            new Asset { Id = "660510ad00baa1f0e906225f", Name = "MacMini CPU-M2 RAM-8G", AssetTypeId = "6602b5508836f41c710e02ed" },
            new Asset { Id = "660510ad00baa1f0e9062261", Name = "Asus CPU-i9 13300K RAM-32G SSD-1T", AssetTypeId = "6602b5508836f41c710e02ed" },
            new Asset { Id = "660510ad00baa1f0e9062262", Name = "ASRock", AssetTypeId = "6602b5508836f41c710e02ed" },
            // Laptop
            new Asset { Id = "660510ad00baa1f0e906225e", Name = "Lenovo CPU-i7 8880U RAM-16G SSD-512G", AssetTypeId = "6602b5508836f41c710e02ee" },
            new Asset { Id = "660510ad00baa1f0e9062260", Name = "Dell CPU-i9 112U RAM-16G SSD-1T", AssetTypeId = "6602b5508836f41c710e02ee" },
            // Monitor
            new Asset { Id = "660510ad00baa1f0e9062263", Name = "Samsung 24", AssetTypeId = "6602b5508836f41c710e02f0" },
            new Asset { Id = "660510ad00baa1f0e9062264", Name = "HP 32", AssetTypeId = "6602b5508836f41c710e02f0" },
            new Asset { Id = "660510ad00baa1f0e9062265", Name = "Dell 32", AssetTypeId = "6602b5508836f41c710e02f0" },
            new Asset { Id = "660510ad00baa1f0e9062266", Name = "LG 27", AssetTypeId = "6602b5508836f41c710e02f0" },
            // Chair
            new Asset { Id = "6605130500baa1f0e906232a", Name = "Dafne China", AssetTypeId = "6602b5508836f41c710e02ef" }
        ]);
    }
}