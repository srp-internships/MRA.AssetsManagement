using MRA.AssetsManagement.Web.Shared;
using MRA.AssetsManagement.Web.Shared.Tags;

namespace MRA.AssetsManagement.Web.Client.Services.Tags
{
    public interface ITagsService
    {
        Task<IEnumerable<MenuItem>> GetMenuItems();

        //Task<GetTags> GetTagById(string id);
        Task Create(CreateTagRequest newTag);
        Task Update(GetTags newTag);
        Task Delete(string id);
        IEnumerable<MenuItem> MenuItems { get; set; }
        IEnumerable<GetTags> Tags { get; set; }

    }
}
