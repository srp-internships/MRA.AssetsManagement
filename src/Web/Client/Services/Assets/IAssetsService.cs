using MRA.AssetsManagement.Web.Shared.Assets;

namespace MRA.AssetsManagement.Web.Client.Services.Assets;

public interface IAssetsService
{
    List<GetAssetSerial> Serials { get; set; }
    Task<IEnumerable<GetAssetSerial>> GetAssetSerials();
}