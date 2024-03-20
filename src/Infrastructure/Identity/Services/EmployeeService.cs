using MRA.AssetsManagement.Domain.Entities;

using Newtonsoft.Json;

namespace MRA.AssetsManagement.Infrastructure.Identity.Services;

public class EmployeeService : IEmployeeService
{
    public Task<List<Employee>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> Create(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RegisterEmployee>> GetEmployeesAsync(string filePath)
    {
        try
        {
            string jsonContent = await File.ReadAllTextAsync("C:Users\\User\\Desktop\\Employees.txt");

            List<RegisterEmployee> employees = JsonConvert.DeserializeObject<List<RegisterEmployee>>(jsonContent);

            return employees;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ошибка при чтении файла: {ex.Message}");
        }
    }
}