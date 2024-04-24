
namespace MRA.AssetsManagement.Web.Shared.Assets
{
    public class PagedList<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
        public int TotalCount { get; set; }
    }
}
