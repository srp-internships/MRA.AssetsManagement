using MRA.AssetsManagement.Domain.Entities.Employee;

public interface IEmployeeService
{
    Task<List<Employee>> GetAll();
    Task<Employee> GetById(string id);
    Task<Employee> GetByEmail(string email);
    Task<string> Create(CreateEmployee createEmployee);
}