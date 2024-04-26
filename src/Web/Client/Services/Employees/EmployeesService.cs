using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MRA.AssetsManagement.Web.Client.Common.Extensions;
using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.Assets;
using MRA.AssetsManagement.Web.Shared.AssetSerials;
using MRA.AssetsManagement.Web.Shared.Employees;
using MRA.BlazorComponents.HttpClient.Services;
using MRA.BlazorComponents.Snackbar.Extensions;

using MudBlazor;

namespace MRA.AssetsManagement.Web.Client.Services.Employees
{
    public class EmployeesService(IHttpClientService httpClient, ISnackbar snackbar, IWebAssemblyHostEnvironment environment) : IEmployeesService
    {
        private readonly string _baseAddress = environment.BaseAddress;

        public async Task<GetEmployee> GetEmployeeByUserName(string userName)
        {
            var response = await httpClient.GetFromJsonAsync<GetEmployee>($"{_baseAddress}api/employees/{userName}");

            return response.Result!;
        }

        public async Task<IEnumerable<GetEmployee>> GetEmployees()
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<GetEmployee>>($"{_baseAddress}api/employees");
            snackbar.ShowIfError(response, "Error was occured");
            return response.Result!;
        }

        public async Task<List<MenuItem>> Fetch()
        {
            var response = await httpClient.GetFromJsonAsync<List<GetEmployee>>($"{_baseAddress}api/employees");
            snackbar.ShowIfError(response, "Error was occured.");

            return response.Result!.Select(mi => mi.ToMenuItem()).ToList();
        }

        public async Task<GetEmployee> Create(CreateEmployeeRequest newEmployee)
        {
            var response = await httpClient.PostAsJsonAsync<GetEmployee>($"{_baseAddress}api/employees", newEmployee);
            snackbar.ShowIfError(response, "Error was occured.");

            return response.Result!;
        }

        public async Task<List<GetAssetSerials>> GetEmployeeAssetsSerials(string userName)
        {
            var response = await httpClient.GetFromJsonAsync<List<GetAssetSerials>>($"{_baseAddress}api/employees/serials/{userName}");
            snackbar.ShowIfError(response, "Occured some errors");
            return response.Result!;
        }
    }
}
