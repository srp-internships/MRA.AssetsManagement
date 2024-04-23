namespace MRA.AssetsManagement.Infrastructure.Migrations;

public interface IMigration
{
    void Up();
    void Down();
}