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
            new AssetType() { Id = "6602b5508836f41c710e02ed", Slug = "pc" ,Name = "PC", ShortName = "PC", Icon = "PC" },
            new AssetType() { Id = "6602b5508836f41c710e02ee", Slug = "laptop" ,Name = "Laptop", ShortName = "LPT", Icon = "Laptop" },
            new AssetType() { Id = "6602b5508836f41c710e02ef", Slug = "chair" ,Name = "Chair", ShortName = "CHR", Icon = "Chair" },
            new AssetType() { Id = "6602b5508836f41c710e02f0", Slug = "monitor" ,Name = "Monitor", ShortName = "MTR", Icon = "Monitor" }
        );
    }
}