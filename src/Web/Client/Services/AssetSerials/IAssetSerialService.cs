using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerialHistory;
using MRA.AssetsManagement.Web.Shared.AssetSerials;

namespace MRA.AssetsManagement.Web.Client.Services.AssetSerials;

public interface IAssetSerialService
{
    Task<GetAssetSerial> GetBySerial(string id);
    Task UpdateSerial(UpdateAssetSerialRequest request);
    Task<IEnumerable<GetAssetSerialHistory>> GetAssetSerialHistories(string serial);
    
}