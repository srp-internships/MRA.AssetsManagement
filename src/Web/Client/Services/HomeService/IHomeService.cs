using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Web.Client.Services.HomeService
{
    public interface IHomeService
    {
        Task<IEnumerable<GetAssetTypeSerial>> Get();
    }
}
