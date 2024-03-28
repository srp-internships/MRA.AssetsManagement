using System.Text;

namespace MRA.AssetsManagement.Web.Client.Common.Extensions;

public static class StringExtensions
{
    public static string ToShortName(this string name)
    {
        return ContractString(StringWithoutVowels(name));
    }
    
    private static string StringWithoutVowels( string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input string cannot be null or empty.");

        StringBuilder contractedForm = new();

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

    private static string ContractString(string str)
    {
        var result = "";

        for (int i = 0; i < str.Length - 1 && result.Length < 3; i++)
        {
            if (str[i] == str[i + 1] && i != str.Length - 2)
                continue;
            result += str[i];
        }

        return result;
    }
}