using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Client.Services.Tags
{
    public interface ITagsService : IFetchMenuItemService
    {
        Task<List<GetTag>> GetAll();
        Task<GetTag> GetTagById(string id);
        Task<GetTag> Create(CreateTagRequest newTag);
        Task<bool> Update(GetTag newTag);
        Task Delete(string id);
    }
}
