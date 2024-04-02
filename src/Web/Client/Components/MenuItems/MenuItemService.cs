
namespace MRA.AssetsManagement.Web.Client.Components.MenuItems;

public class MenuItemService : IMenuItemService
{
    public event Action<MenuItemEvent, MenuItem>? Changed;

    public void OnChangeMenuItem(MenuItemEvent menuItemEvent, MenuItem menuItem)
    {
        Changed?.Invoke(menuItemEvent, menuItem);
    }
}

public enum MenuItemEvent
{
    Add,
    Update,
    Delete
}