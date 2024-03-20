using MRA.AssetsManagement.Domain.Entities;

public interface IEmployeeService
{
    Task<List<Employee>> GetAll();
    Task<Employee> GetById(string id);
    Task<Employee> GetByEmail(string email);
    Task<Employee> Create(Employee employee);
    Task<bool> Delete(string id);
    Task<List<RegisterEmployee>> GetEmployeesAsync(string filePath);

}