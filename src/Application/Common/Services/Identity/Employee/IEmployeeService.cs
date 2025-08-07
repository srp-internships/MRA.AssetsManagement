using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Common.Services.Identity.Employee;

public interface IEmployeeService
{
    Task<List<Domain.Entities.Employee.Employee>> GetAll();
    Task<Domain.Entities.Employee.Employee?> GetByUserName(string userName);
    Task<Domain.Entities.Employee.Employee?> GetByEmail(string email);
    Task<string> Create(CreateEmployeeRequest createEmployeeRequest);
    Task<Domain.Entities.Employee.Employee> Update(MRA.AssetsManagement.Web.Shared.Employees.GetEmployee updatedEmployee);
    Task<Domain.Entities.Employee.Employee?> GetById(string id);
    Task Update(Domain.Entities.Employee.Employee employee);
}