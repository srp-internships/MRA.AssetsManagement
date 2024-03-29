using MRA.AssetsManagement.Web.Client.Shared.MenuItems;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Client.Services.Tags
{
    public interface ITagsService : IFetchMenuItemService
    {
        Task<GetTag> GetTagById(string id);
        Task Create(CreateTagRequest newTag);
        Task Update(GetTag newTag);
        Task Delete(string id);
    }
}
