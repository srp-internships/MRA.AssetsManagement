using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities
{
    public class AssetEntitySeeder : EntitySeeder<Asset>
    {
        public AssetEntitySeeder(IRepository<Asset> repository) : base(repository)
        {
        }

        public override async Task Development()
        {
            if (await _repository.AnyAsync()) return;

            await _repository.CreateAsync(default,
                new Asset() { Id = "660510ad00baa1f0e906225d", Name = "MSI", AssetTypeId = "6602b5508836f41c710e02ed" },
                new Asset() { Id = "660510ad00baa1f0e906225e", Name = "Lenova", AssetTypeId = "6602b5508836f41c710e02ee" },
                new Asset() { Id = "660510ad00baa1f0e906225f", Name = "MacMini", AssetTypeId = "6602b5508836f41c710e02ed" },
                new Asset() { Id = "660510ad00baa1f0e9062260", Name = "Dell", AssetTypeId = "6602b5508836f41c710e02ee" },
                new Asset() { Id = "660510ad00baa1f0e9062261", Name = "Asus", AssetTypeId = "6602b5508836f41c710e02ed" },
                new Asset() { Id = "660510ad00baa1f0e9062262", Name = "ASRock", AssetTypeId = "6602b5508836f41c710e02ed" },
                new Asset() { Id = "660510ad00baa1f0e9062263", Name = "Samsung", AssetTypeId = "6602b5508836f41c710e02f0" },
                new Asset() { Id = "660510ad00baa1f0e9062264", Name = "hp", AssetTypeId = "6602b5508836f41c710e02f0" },
                new Asset() { Id = "660510ad00baa1f0e9062265", Name = "Dell", AssetTypeId = "6602b5508836f41c710e02f0" },
                new Asset() { Id = "660510ad00baa1f0e9062266", Name = "LG", AssetTypeId = "6602b5508836f41c710e02f0" },
                new Asset() { Id = "6605130500baa1f0e906232a", Name = "Dafna", AssetTypeId = "6602b5508836f41c710e02ef" }
            );
        }
    }
}
