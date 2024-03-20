using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class AssetTypeEntitySeeder : EntitySeeder<AssetType>
{
    public AssetTypeEntitySeeder(IRepository<AssetType> repository) : base(repository)
    {
    }

    public override async Task Development()
    {
        if (await _repository.Any()) return;

        await _repository.CreateAsync(default,
            new AssetType() { Name = "PC", ShortName = "PC", Icon = "pc" },
            new AssetType() { Name = "Laptop", ShortName = "LPT", Icon = "laptop" },
            new AssetType() { Name = "Chair", ShortName = "CHR", Icon = "chair" },
            new AssetType() { Name = "Monitor", ShortName = "MTR", Icon = "monitor" }
        );
    }
}