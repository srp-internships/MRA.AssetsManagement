namespace MRA.AssetsManagement.Web.Shared.Assets
{
    public class AssetsFilterOptions
    {
        public string? Status { get; set; }
        public string? Serial { get; set; }
        public string? Tags { get; set; }
        public string? Type { get; set; }
        public string? AssetName { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
    }
}
