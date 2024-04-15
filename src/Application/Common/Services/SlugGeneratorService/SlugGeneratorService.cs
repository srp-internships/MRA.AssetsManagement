using Slugify;

namespace MRA.AssetsManagement.Application.Common.Services.SlugGeneratorService
{
    public class SlugGeneratorService : ISlugGeneratorService
    {
        private static readonly SlugHelper SlugHelper = new();
        public string GenerateSlug(string inputText)
        {
            var translatedText = NickBuhro.Translit.Transliteration.CyrillicToLatin(inputText);

            return SlugHelper.GenerateSlug(translatedText);
        }
    }
}
