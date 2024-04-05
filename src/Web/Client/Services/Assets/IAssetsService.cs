using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Web.Client.Services.Assets;

public interface IAssetsService
{
    Task<IEnumerable<GetAssetSerial>> GetAssetSerials();
}