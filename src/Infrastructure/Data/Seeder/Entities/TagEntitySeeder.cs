using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class TagEntitySeeder : EntitySeeder<Tag>
{
    public TagEntitySeeder(IRepository<Tag> repository) : base(repository)
    {
    }

    public async override Task Development()
    {
        if (await _repository.Any()) return;
        
        await _repository.CreateAsync(default,
            new () { Name = "outdated", Color = "#DD0000" },
            new () { Name = "fast", Color = "#007C00" },
            new () { Name = "issues", Color = "#5600AC" }
        );
    }
}