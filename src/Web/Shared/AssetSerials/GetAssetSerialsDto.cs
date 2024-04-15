using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA.AssetsManagement.Web.Shared.AssetSerials
{
    public class GetAssetSerialsDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Serial { get; set; } = null!;
    }
}
