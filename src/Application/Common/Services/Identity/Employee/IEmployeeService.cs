using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Common.Services.Identity.Employee;

public interface IEmployeeService
{
    Task<List<Domain.Entities.Employee.Employee>> GetAll();
    Task<Domain.Entities.Employee.Employee?> GetByUserName(string userName);
    Task<Domain.Entities.Employee.Employee?> GetByEmail(string email);
    Task<string> Create(CreateEmployeeRequest createEmployeeRequest);
}