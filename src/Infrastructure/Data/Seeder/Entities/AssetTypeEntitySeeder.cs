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
        if (await _repository.AnyAsync()) return;

        await _repository.CreateAsync(default,
            new AssetType() { Id = "6602b5508836f41c710e02ed", Name = "PC", ShortName = "PC", Icon = "pc" },
            new AssetType() { Id = "6602b5508836f41c710e02ee", Name = "Laptop", ShortName = "LPT", Icon = "laptop" },
            new AssetType() { Id = "6602b5508836f41c710e02ef", Name = "Chair", ShortName = "CHR", Icon = "chair" },
            new AssetType() { Id = "6602b5508836f41c710e02f0", Name = "Monitor", ShortName = "MTR", Icon = "monitor" }
        );
    }
}