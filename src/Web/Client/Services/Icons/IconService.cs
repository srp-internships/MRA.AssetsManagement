
namespace MRA.AssetsManagement.Web.Client.Services.Icons
{
    public class IconService : IIconService
    {
        private readonly Dictionary<string, string> _icons;

        public IconService()
        {
            _icons = new Dictionary<string, string>
            {
                { "icon1", "Icons.Material.Outlined.LaptopChromebook" },
                { "icon2", "Icons.Material.TwoTone.ChairAlt" }
            };
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

        public IReadOnlyDictionary<string, string> GetIcons()
        {
            return _icons;
        }
    }
}
