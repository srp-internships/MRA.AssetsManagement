using MRA.AssetsManagement.Web.Client.Shared.MenuItems;

namespace MRA.AssetsManagement.Web.Client.Services.Tags;

public interface ITagsService : IFetchService
{
    
}

public class Tag
{
    public string Title => "Outdate";
    public string Icon => "Laptop";
    public string Route => "/settings/tags/345345";
}


public class TagService: ITagsService
{
    public async Task<IEnumerable<MenuItem>> Fetch()
    {
        await Task.Delay(2000);
        return new[] { MenuItem.FromTag(new Tag()) };
    }
}