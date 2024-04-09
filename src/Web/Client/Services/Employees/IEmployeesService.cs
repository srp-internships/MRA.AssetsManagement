using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Web.Client.Services.Employees
{
    public interface IEmployeesService : IFetchMenuItemService
    {
        Task<GetEmployee> GetEmployeeById(string id);
        Task<IEnumerable<GetEmployee>> GetEmployees();
        Task<GetEmployee> Create(CreateEmployeeRequest newEmployee);
    }
}
