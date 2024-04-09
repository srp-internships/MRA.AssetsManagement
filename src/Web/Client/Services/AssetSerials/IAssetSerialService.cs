using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Web.Client.Services.AssetSerials;

public interface IAssetSerialService
{
    event Action OnChange;
    Task<GetAssetSerial> GetBySerial(string id);
    Task UpdateSerial(UpdateAssetSerialRequest request);
}