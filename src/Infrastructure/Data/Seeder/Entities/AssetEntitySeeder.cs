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
            if (await _repository.Any()) return;

            await _repository.CreateAsync(default,
                new Asset() { Name = "MSI", AssetTypeId = "6602b5508836f41c710e02ed" },
                new Asset() { Name = "Lenova", AssetTypeId = "6602b5508836f41c710e02ee" },
                new Asset() { Name = "LG", AssetTypeId = "6602b5508836f41c710e02f0" }
            );
        }
    }
}
