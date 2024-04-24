using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class VersionEntitySeeder : EntitySeeder<AppVersion>
{
    private readonly IApplicationDbContext _context;

    public VersionEntitySeeder(IApplicationDbContext context) : base(context.DbVersions)
    {
        _context = context;
    }
    public override async Task Development()
    {
        if (await _repository.AnyAsync()) return;
     
        await _repository.CreateAsync(default, [
            // PC
            new AppVersion(){Version = 1}
        ]);
    }
}