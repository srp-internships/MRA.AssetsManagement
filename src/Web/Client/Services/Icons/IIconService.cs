namespace MRA.AssetsManagement.Web.Client.Services.Icons
{
    public interface IIconService
    {
        IReadOnlyDictionary<string, string> GetIcons();
        string GetIcon(string key);
    }
}
