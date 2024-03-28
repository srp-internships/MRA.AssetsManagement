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

        var assetTypes = await _context.AssetTypes.GetAllAsync();

        var laptopSerials =
            (await _context.AssetSerials.GetAllAsync(x =>
                x.Asset.AssetTypeId == assetTypes.First(at => at.Name == "Laptop").Id)).ToList();
        
        var monitorSerials = (await _context.AssetSerials.GetAllAsync(x =>
            x.Asset.AssetTypeId == assetTypes.First(at => at.Name == "Monitor").Id)).ToList();

        await _repository.CreateAsync(default, [
            new AssetHistory { AssetSerial = laptopSerials[0], DateTime = DateTime.Now.AddMonths(-2) },
            new AssetHistory { AssetSerial = laptopSerials[1], DateTime = DateTime.Now.AddMonths(-2) },
            new AssetHistory { AssetSerial = laptopSerials[2], DateTime = DateTime.Now.AddMonths(-2) },
            new AssetHistory { AssetSerial = monitorSerials[0], DateTime = DateTime.Now.AddMonths(-2) }
        ]);

        laptopSerials.ForEach(x =>
        {
            x.Status = AssetStatus.Taken;
            Task.Run(async () => await _context.AssetSerials.UpdateAsync(x));
        });

        monitorSerials.ForEach(x =>
        {
            x.Status = AssetStatus.Taken;
            Task.Run(async () => await _context.AssetSerials.UpdateAsync(x));
        });

        await _repository.CreateAsync(default, [
            new AssetHistory { AssetSerial = laptopSerials[0], DateTime = DateTime.Now.AddDays(-55) },
            new AssetHistory { AssetSerial = laptopSerials[1], DateTime = DateTime.Now.AddDays(-55) },
            new AssetHistory { AssetSerial = laptopSerials[2], DateTime = DateTime.Now.AddDays(-55) },
            new AssetHistory { AssetSerial = monitorSerials[0], DateTime = DateTime.Now.AddDays(-55) }
        ]);

        monitorSerials[0].Status = AssetStatus.Broken;

        await _repository.CreateAsync(default,
            [new() { AssetSerial = monitorSerials[0], DateTime = DateTime.Now.AddDays(-10) }]);

        await _context.AssetSerials.UpdateAsync(monitorSerials[0]);
    }
}