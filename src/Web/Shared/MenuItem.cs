using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA.AssetsManagement.Web.Shared
{
    public class MenuItem
    {
        public string Route { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public bool Archived { get; set; } = false;
    }
}
