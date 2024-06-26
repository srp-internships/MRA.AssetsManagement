namespace MRA.AssetsManagement.Infrastructure.Data;

public class MongoDbOption
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public bool Seeder { get; set; } = false;
    public int Version { get; set; }
}