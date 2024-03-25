using MRA.AssetsManagement.Domain.Entities;

public interface IEmployeeService
{
    Task<List<EmployeeResponse>> GetAll(string token);
    Task<EmployeeResponse> GetById(string id,string token);
    Task<EmployeeResponse> GetByEmail(string email, string token);
    Task<string> Create(RegisterEmployee registerEmployee,string token);
}