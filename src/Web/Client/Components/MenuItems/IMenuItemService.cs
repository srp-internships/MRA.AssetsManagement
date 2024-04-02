namespace MRA.AssetsManagement.Web.Client.Components.MenuItems
{
    public interface IMenuItemService
    {
        event Action<MenuItemEvent, MenuItem>? Changed;
        void OnChangeMenuItem(MenuItemEvent menuItemEvent, MenuItem menuItem);
    }
}