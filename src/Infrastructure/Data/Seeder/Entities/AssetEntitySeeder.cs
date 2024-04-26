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

        await _repository.CreateAsync(default, [
            // PC
            new Asset
            {
                Id = "660510ad00baa1f0e906225d",
                Name = "MSI",
                Properties =
                [
                    new Properties { Label = "Model", Value = "Prix" },
                    new Properties { Label = "RAM", Value = "32Gb" },
                    new Properties { Label = "CPU", Value = "i9-13300K" },
                    new Properties { Label = "SSD", Value = "1T" },
                ],
                AssetTypeId = "6602b5508836f41c710e02ed"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e906225f",
                Name = "MacMini",
                Properties = [
                    new Properties { Label = "Model", Value = "Pro" },
                    new Properties { Label = "RAM", Value = "8Gb" },
                    new Properties { Label = "CPU", Value = "M2 Pro" },
                    new Properties { Label = "SSD", Value = "512Gb" },
                ],
                AssetTypeId = "6602b5508836f41c710e02ed"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e9062261",
                Name = "Asus",
                Properties = [
                    new Properties { Label = "Model", Value = "ROG Strix" },
                    new Properties { Label = "RAM", Value = "32Gb" },
                    new Properties { Label = "CPU", Value = "i9-13300K" },
                    new Properties { Label = "SSD", Value = "512Gb" },
                    new Properties { Label = "GPU", Value = "RTX 3080 10 ГБ" },
                ],
                AssetTypeId = "6602b5508836f41c710e02ed"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e9062262",
                Name = "Acer",
                Properties = [
                    new Properties { Label = "Model", Value = "Aspire" },
                    new Properties { Label = "RAM", Value = "8Gb" },
                    new Properties { Label = "CPU", Value = "i3-1215U" },
                    new Properties { Label = "SSD", Value = "256Gb" },
                ],
                AssetTypeId = "6602b5508836f41c710e02ed"
            },
            // Laptop
            new Asset
            {
                Id = "660510ad00baa1f0e906225e",
                Name = "Lenovo",
                Properties = [
                    new Properties { Label = "Model", Value = "Thinkpad" },
                    new Properties { Label = "RAM", Value = "16Gb" },
                    new Properties { Label = "Size, Inch", Value = "15.6" },
                    new Properties { Label = "CPU", Value = "i5-8250U" },
                    new Properties { Label = "SSD", Value = "256Gb" },
                ],
                AssetTypeId = "6602b5508836f41c710e02ee"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e9062260",
                Name = "Dell",
                Properties = [
                    new Properties { Label = "Model", Value = "XPS" },
                    new Properties { Label = "RAM", Value = "16Gb" },
                    new Properties { Label = "Size, Inch", Value = "14" },
                    new Properties { Label = "CPU", Value = "i5-1120U" },
                    new Properties { Label = "SSD", Value = "1T" },
                ],
                AssetTypeId = "6602b5508836f41c710e02ee"
            },
            // Monitor
            new Asset
            {
                Id = "660510ad00baa1f0e9062263",
                Name = "Samsung",
                Properties = [
                    new Properties { Label = "Model", Value = "Solo"},
                    new Properties { Label = "Size, Inch", Value = "24"}
                ],
                AssetTypeId = "6602b5508836f41c710e02f0"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e9062264",
                Name = "HP",
                Properties = [
                    new Properties { Label = "Model", Value = "Tatu"},
                    new Properties { Label = "Size, Inch", Value = "27"}
                ],
                AssetTypeId = "6602b5508836f41c710e02f0"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e9062265",
                Name = "Dell",
                Properties = [
                    new Properties { Label = "Model", Value = "Ultra"},
                    new Properties { Label = "Size, Inch", Value = "32"}
                ],
                AssetTypeId = "6602b5508836f41c710e02f0"
            },
            new Asset
            {
                Id = "660510ad00baa1f0e9062266",
                Name = "LG",
                Properties = [
                    new Properties { Label = "Model", Value = "Nano"},
                    new Properties { Label = "Size, Inch", Value = "22"}
                ],
                AssetTypeId = "6602b5508836f41c710e02f0"
            },
            // Chair
            new Asset
            {
                Id = "6605130500baa1f0e906232a",
                Name = "Dafne",
                Properties = [
                    new Properties { Label = "Model", Value = "CH9944"},
                    new Properties { Label = "Color", Value = "Black"}
                ],
                AssetTypeId = "6602b5508836f41c710e02ef"
            }
        ]);
    }
}