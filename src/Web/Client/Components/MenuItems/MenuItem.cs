namespace MRA.AssetsManagement.Web.Client.Components.MenuItems;

public record MenuItem(string Id, string Title, string Icon, string Route, bool Disabled = false);