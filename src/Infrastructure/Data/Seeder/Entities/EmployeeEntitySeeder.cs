using BCrypt.Net;
using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class EmployeeEntitySeeder : EntitySeeder<Employee>
{
    public EmployeeEntitySeeder(IRepository<Employee> repository) : base(repository)
    {
    }

    public async override Task Development()
    {
        if (await _repository.AnyAsync()) return;
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123", BCrypt.Net.BCrypt.GenerateSalt(12));
        
        await _repository.CreateAsync(default,
            new Employee 
            { 
                Id = Guid.NewGuid().ToString(),
                UserName = "admin", 
                Email = "admin@example.com",
                Password = hashedPassword,
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "+1234567890"
            }
        );
    }
    
    public async override Task Production()
    {
        if (await _repository.AnyAsync()) return;
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123", BCrypt.Net.BCrypt.GenerateSalt(12));
        
        await _repository.CreateAsync(default,
            new Employee 
            { 
                Id = Guid.NewGuid().ToString(),
                UserName = "admin", 
                Email = "admin@example.com",
                Password = hashedPassword,
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "+1234567890"
            }
        );
    }
} 