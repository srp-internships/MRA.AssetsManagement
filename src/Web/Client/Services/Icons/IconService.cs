using System.Collections.Generic;

namespace MRA.AssetsManagement.Web.Client.Services.Icons
{
    public class IconService : IIconService
    {
        private readonly Dictionary<string, string> _icons = new Dictionary<string, string>
        {
            { "Laptop", "<g><rect fill=\"none\" height=\"24\" width=\"24\" x=\"0\"/></g><g><g><path d=\"M20,18c1.1,0,2-0.9,2-2V6c0-1.1-0.9-2-2-2H4C2.9,4,2,4.9,2,6v10c0,1.1,0.9,2,2,2H0v2h24v-2H20z M4,6h16v10H4V6z\"/></g></g>" },
            { "LaptopChromebook", "<g><rect fill=\"none\" height=\"24\" width=\"24\" x=\"0\"/></g><g><g><path d=\"M20,18c1.1,0,2-0.9,2-2V6c0-1.1-0.9-2-2-2H4C2.9,4,2,4.9,2,6v10c0,1.1,0.9,2,2,2H0v2h24v-2H20z M4,6h16v10H4V6z\"/></g></g>" }
            
        };

        public IReadOnlyDictionary<string, string> GetIcons()
        {
            return _icons;
        }

        public string GetIcon(string key)
        {
            if (_icons.ContainsKey(key))
            {
                return _icons[key];
            }
            else
            {
                return null; 
            }
        }
    }
}
