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
    event Action<MenuItemEvent, MenuItem> Changed;
    void OnChangeMenuItem(MenuItemEvent @event, MenuItem menuItem);
}

public class MenuItemService : IMenuItemService
{
    public event Action<MenuItemEvent, MenuItem>? Changed;
}