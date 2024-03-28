using MRA.AssetsManagement.Web.Client.Services.Tags;
using MRA.AssetsManagement.Web.Shared.AssetTypes;

namespace MRA.AssetsManagement.Web.Client.Shared.MenuItems;

public record MenuItem(string Title, string Icon, string Route, bool Disabled = false)
{
    public static MenuItem FromAssetType(GetAssetType assetType)
    {
        return new MenuItem(assetType.Name, assetType.Icon, $"/settings/asset-types/{assetType.Id}", assetType.Archived);
    }
    
    public static MenuItem FromTag(Tag tag)
    {
        return new MenuItem(tag.Title, tag.Icon, $"/settings/tags/234345");
    }
}

public interface IFetchService
{
    Task<IEnumerable<MenuItem>> Fetch();
}

public interface IMenuItemService
{
    event Action RefreshRequested;
    void SetFetcher(IFetchService fetchService);

    IFetchService? Service { get; }
}

public class MenuItemsService : IMenuItemService
{
    public event Action? RefreshRequested;

    public void SetFetcher(IFetchService fetchService)
    {
        Service = fetchService;
        RefreshRequested?.Invoke();
    }

    public IFetchService? Service { get; private set; }
}