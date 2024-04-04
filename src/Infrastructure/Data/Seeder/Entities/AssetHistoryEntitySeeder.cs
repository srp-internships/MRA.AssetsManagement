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

        var assetSerials = (await _context.AssetSerials.GetAllAsync()).ToList();

        List<AssetHistory> histories = [];

        foreach (var serial in assetSerials)
        {
            histories.Add(new AssetHistory
            {
                AssetSerial = new()
                {
                    Id = serial.Id, Asset = serial.Asset, Serial = serial.Serial, Status = AssetStatus.Available
                },
                DateTime = DateTime.Now.AddDays(new Random().Next(-90, -50))
            });
            if (serial.Status == AssetStatus.Available) continue;

            histories.Add(new AssetHistory
            {
                AssetSerial = new()
                {
                    Id = serial.Id, Asset = serial.Asset, Serial = serial.Serial, Status = AssetStatus.Taken
                },
                DateTime = DateTime.Now.AddDays(new Random().Next(-50, -40))
            });
            if (serial.Status == AssetStatus.Taken) continue;

            histories.Add(new AssetHistory
            {
                AssetSerial = new AssetSerial
                {
                    Id = serial.Id, Asset = serial.Asset, Serial = serial.Serial, Status = AssetStatus.Broken
                },
                DateTime = DateTime.Now.AddDays(new Random().Next(-39, -20))
            });
            if (serial.Status == AssetStatus.Broken) continue;

            histories.Add(new AssetHistory
            {
                AssetSerial = new AssetSerial
                {
                    Id = serial.Id,
                    Asset = serial.Asset,
                    Serial = serial.Serial,
                    Status = AssetStatus.Deprecated
                },
                DateTime = DateTime.Now.AddDays(new Random().Next(-19, -2))
            });
        }

        await _repository.CreateAsync(default, histories.ToArray());
    }
}