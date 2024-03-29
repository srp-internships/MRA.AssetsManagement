namespace MRA.AssetsManagement.Web.Client.Shared.MenuItems;

public interface IFetchMenuItemService
{
    Task<IEnumerable<MenuItem>> Fetch();
}

public enum MenuItemEvent
{
    Add,
    Update,
    Delete
}

public interface IMenuItemService
{
    event Action<KeyValuePair<MenuItemEvent, MenuItem>> Changed;
}

public class MenuItemService : IMenuItemService
{
    public event Action<KeyValuePair<MenuItemEvent, MenuItem>>? Changed;
}