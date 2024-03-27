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
        if (await _repository.Any()) return;
        
        var laptopAssetType = (await _context.AssetTypes.GetAllAsync(x => x.Name == "Laptop")).FirstOrDefault();
        var assets = await _context.Assets.GetAllAsync(x => x.AssetTypeId == laptopAssetType!.Id);

        await _repository.CreateAsync(default, [
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = assets.ElementAt(0),
                Serial = laptopAssetType!.ShortName + "000001"
            },
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = assets.ElementAt(1),
                Serial = laptopAssetType!.ShortName + "000002"
            },
            new AssetSerial
            {
                Status = AssetStatus.Available,
                Asset = assets.ElementAt(2),
                Serial = laptopAssetType!.ShortName + "000003"
            }
        ]);
    }
}