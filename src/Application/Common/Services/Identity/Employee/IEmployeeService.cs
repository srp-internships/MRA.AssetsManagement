using MRA.AssetsManagement.Domain.Entities;

public interface IEmployeeService
{
    Task<List<EmployeeResponse>> GetAll();
    Task<EmployeeResponse> GetById(string id);
    Task<EmployeeResponse> GetByEmail(string email);
    Task<string> Create(RegisterEmployee registerEmployee);
}