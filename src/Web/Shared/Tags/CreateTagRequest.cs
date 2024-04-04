using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA.AssetsManagement.Web.Shared.Tags
{
    public class CreateTagRequest
    {
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
