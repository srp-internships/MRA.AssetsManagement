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
        
        var laptopAssetType = (await _context.AssetTypes.GetAllAsync(x => x.Name == "Laptop")).FirstOrDefault();
        var monitorAssetType = (await _context.AssetTypes.GetAllAsync(x => x.Name == "Monitor")).FirstOrDefault();
        
        var laptops = await _context.Assets.GetAllAsync(x => x.AssetTypeId == laptopAssetType!.Id);
        var monitors = await _context.Assets.GetAllAsync(x => x.AssetTypeId == monitorAssetType!.Id);
        
        await _repository.CreateAsync(default, [
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = laptops.ElementAt(0),
                Serial = laptopAssetType!.ShortName + "000001"
            },
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = laptops.ElementAt(1),
                Serial = laptopAssetType!.ShortName + "000002"
            },
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = laptops.ElementAt(2),
                Serial = laptopAssetType!.ShortName + "000003"
            },
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = monitors.ElementAt(0),
                Serial = monitorAssetType!.ShortName + "000001"
            }
        ]);
    }
}