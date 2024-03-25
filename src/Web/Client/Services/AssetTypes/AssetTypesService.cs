using System.Text;

using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.AssetTypes
{
    public class AssetTypesService(IHttpClientService httpClient, ISnackbar snackbar, IConfiguration configuration) : IAssetTypesService
    {
        public string GenerateShortName(string name)
        {
            return ContractString(StringWithoutVowels(name));
        }

        public async Task<AssetType> GetAssetTypeById(string id)
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<AssetType>>("https://localhost:7098/api/assettypes");
            snackbar.ShowIfError(response, "Error was occured.");
            var assetType = response.Result!.FirstOrDefault(at => at.Id == id);

            return assetType!;
        }

        public async Task<List<AssetType>> GetAssetTypes()
        {
            var response = await httpClient.GetFromJsonAsync<List<AssetType>>("https://localhost:7098/api/assettypes");
            snackbar.ShowIfError(response, "Error was occured.");

            return response.Result!;
        }


        private string StringWithoutVowels(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string cannot be null or empty.");

            StringBuilder contractedForm = new StringBuilder();

            string uppercaseInput = input.ToUpper();

            contractedForm.Append(uppercaseInput[0]);

            for (int i = 1; i < uppercaseInput.Length; i++)
            {
                if ("AEIOU".Contains(uppercaseInput[i]) || char.IsWhiteSpace(uppercaseInput[i]))
                    continue;

                contractedForm.Append(uppercaseInput[i]);
            }

            return contractedForm.ToString();
        }

        private string ContractString(string str)
        {
            var result = "";

            for (int i = 0; i < str.Length - 1 && result.Length < 3; i++)
            {
                if (str[i] == str[i + 1] && i != str.Length - 2)
                    continue;
                else
                    result += str[i];
            }

            return result;
        }
    }
}
