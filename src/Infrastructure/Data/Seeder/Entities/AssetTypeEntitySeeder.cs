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

        var pcProperties = new List<Properties> { 
            new Properties { Label = "Model", Value = "" }, 
            new Properties { Label = "RAM", Value = "16Gb" }, 
            new Properties { Label = "CPU", Value = "" },
            new Properties { Label = "GPU", Value = "" },
            new Properties { Label = "SSD", Value = "512Gb" },
        };

        var laptopProperties = new List<Properties> {
            new Properties { Label = "Model", Value = ""},
            new Properties { Label = "Size, Inch", Value = "15.6"},
            new Properties { Label = "CPU", Value = ""},
            new Properties { Label = "RAM", Value = ""},
            new Properties { Label = "SSD", Value = "512Gb" }
        };

        var monitorProperties = new List<Properties> {
            new Properties { Label = "Model", Value = ""},
            new Properties { Label = "Size, Inch", Value = "24"}
        };

        var chairProperties = new List<Properties> {
            new Properties { Label = "Model", Value = ""},
            new Properties { Label = "Color", Value = ""},
        };

        await _repository.CreateAsync(default,
            new AssetType() { Id = "6602b5508836f41c710e02ed", Slug = "pc", Name = "PC", ShortName = "PC", Properties = pcProperties, Icon = "PC" },
            new AssetType() { Id = "6602b5508836f41c710e02ee", Slug = "laptop", Name = "Laptop", ShortName = "LPT", Properties = laptopProperties, Icon = "Laptop" },
            new AssetType() { Id = "6602b5508836f41c710e02ef", Slug = "chair", Name = "Chair", ShortName = "CHR", Properties = chairProperties, Icon = "Chair" },
            new AssetType() { Id = "6602b5508836f41c710e02f0", Slug = "monitor", Name = "Monitor", ShortName = "MTR", Properties = monitorProperties, Icon = "Monitor" }
        );
    }
}