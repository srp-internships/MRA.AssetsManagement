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
        if (await _repository.Any()) return;
     
        var laptopAssetType = (await _context.AssetTypes.GetAllAsync(x => x.Name == "Laptop")).FirstOrDefault();
        var monitorAssetType = (await _context.AssetTypes.GetAllAsync(x => x.Name == "Monitor")).FirstOrDefault();

        await _repository.CreateAsync(default, [
            new Asset { Name = "Dell XPS 15", AssetTypeId = laptopAssetType!.Id },
            new Asset { Name = "Lenovo ThinkPad", AssetTypeId = laptopAssetType!.Id },
            new Asset { Name = "Macbook Pro", AssetTypeId = laptopAssetType!.Id },
            new Asset { Name = "LG 22EA53", AssetTypeId =  monitorAssetType!.Id }
        ]);
    }
}