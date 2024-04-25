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

        var pcProperties = (await _context.AssetTypes.GetAsync("6602b5508836f41c710e02ed")).Properties;
        var laptopProperties = (await _context.AssetTypes.GetAsync("6602b5508836f41c710e02ee")).Properties;
        var monitorProperties = (await _context.AssetTypes.GetAsync("6602b5508836f41c710e02f0")).Properties;
        var chairProperties = (await _context.AssetTypes.GetAsync("6602b5508836f41c710e02ef")).Properties;

        await _repository.CreateAsync(default, [
            // PC
            new Asset { Id = "660510ad00baa1f0e906225d", Name = "MSI CPU-i9 13300K RAM-32G SSD-1T", Properties = pcProperties, AssetTypeId = "6602b5508836f41c710e02ed" },
            new Asset { Id = "660510ad00baa1f0e906225f", Name = "MacMini CPU-M2 RAM-8G", Properties = pcProperties, AssetTypeId = "6602b5508836f41c710e02ed" },
            new Asset { Id = "660510ad00baa1f0e9062261", Name = "Asus CPU-i9 13300K RAM-32G SSD-1T", Properties = pcProperties, AssetTypeId = "6602b5508836f41c710e02ed" },
            new Asset { Id = "660510ad00baa1f0e9062262", Name = "ASRock", Properties = pcProperties, AssetTypeId = "6602b5508836f41c710e02ed" },
            // Laptop
            new Asset { Id = "660510ad00baa1f0e906225e", Name = "Lenovo CPU-i7 8880U RAM-16G SSD-512G", Properties = laptopProperties, AssetTypeId = "6602b5508836f41c710e02ee" },
            new Asset { Id = "660510ad00baa1f0e9062260", Name = "Dell CPU-i9 112U RAM-16G SSD-1T", Properties = laptopProperties, AssetTypeId = "6602b5508836f41c710e02ee" },
            // Monitor
            new Asset { Id = "660510ad00baa1f0e9062263", Name = "Samsung 24", Properties = monitorProperties, AssetTypeId = "6602b5508836f41c710e02f0" },
            new Asset { Id = "660510ad00baa1f0e9062264", Name = "HP 32", Properties = monitorProperties, AssetTypeId = "6602b5508836f41c710e02f0" },
            new Asset { Id = "660510ad00baa1f0e9062265", Name = "Dell 32", Properties = monitorProperties, AssetTypeId = "6602b5508836f41c710e02f0" },
            new Asset { Id = "660510ad00baa1f0e9062266", Name = "LG 27", Properties = monitorProperties, AssetTypeId = "6602b5508836f41c710e02f0" },
            // Chair
            new Asset { Id = "6605130500baa1f0e906232a", Name = "Dafne China", Properties = chairProperties, AssetTypeId = "6602b5508836f41c710e02ef" }
        ]);
    }
}