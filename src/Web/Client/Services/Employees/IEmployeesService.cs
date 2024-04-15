using MRA.AssetsManagement.Web.Client.Components.MenuItems;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Web.Client.Services.Employees
{
    public interface IEmployeesService : IFetchMenuItemService
    {
        Task<GetEmployee> GetEmployeeByUserName(string userName);
        Task<GetEmployee> Create(CreateEmployeeRequest newEmployee);
        Task<IEnumerable<GetEmployee>> GetEmployees();
    }
}
