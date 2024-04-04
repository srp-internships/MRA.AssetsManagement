namespace MRA.AssetsManagement.Web.Client.Components.MenuItems;

public interface IFetchMenuItemService
{
    Task<List<MenuItem>> Fetch();
}
