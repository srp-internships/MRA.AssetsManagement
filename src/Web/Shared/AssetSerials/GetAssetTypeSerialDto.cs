using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA.AssetsManagement.Web.Shared.AssetSerials
{
    public class GetAssetTypeSerialDto
    {
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public List<GetAssetSerialsDto> AssetSerialsDto { get; set; } = null!;
    }
}
