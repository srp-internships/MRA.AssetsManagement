using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Web.Shared.Employees;

public interface IEmployeeService
{
    Task<List<Employee>> GetAll();
    Task<Employee> GetById(string id);
    Task<Employee> GetByEmail(string email);
    Task<string> Create(CreateEmployeeRequest createEmployeeRequest);
}