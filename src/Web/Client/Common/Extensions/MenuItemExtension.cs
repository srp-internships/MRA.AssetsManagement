using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.AssetTypes;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Client.Common.Extensions;

public static class MenuItemExtension
{
    public static MenuItem ToMenuItem(this GetAssetType assetType)
    {
        return new MenuItem(assetType.Id, assetType.Name, assetType.Icon, $"/settings/asset-types/{assetType.Id}");
    }
    
    public static MenuItem ToMenuItem(this GetTag tag)
    {
        return new MenuItem(tag.Id, tag.Name, "", $"/settings/tags/{tag.Id}");
    }
}