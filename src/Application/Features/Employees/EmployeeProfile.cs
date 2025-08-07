using AutoMapper;
using MRA.AssetsManagement.Domain.Entities.Employee;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Features.Employees;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployeeRequest, Employee>();
        CreateMap<Employee, CreateEmployeeRequest>();
        CreateMap<Employee, EmployeeResponse>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        CreateMap<Employee, GetEmployee>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            .ForMember(d => d.CanLogin, o => o.MapFrom(s => s.CanLogin));
        CreateMap<GetEmployee, Employee>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => GetFirstName(s.FullName)))
            .ForMember(d => d.LastName, o => o.MapFrom(s => GetLastName(s.FullName)))
            .ForMember(d => d.CanLogin, o => o.MapFrom(s => s.CanLogin));
        CreateMap<CreateEmployeeRequest, GetEmployee>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        CreateMap<UpdateEmployeeRequest, Employee>()
            .ForMember(d => d.CanLogin, o => o.MapFrom(s => s.CanLogin));
    }

    private static string GetFirstName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
        var parts = fullName.Split(' ');
        return parts.Length > 0 ? parts[0] : string.Empty;
    }
    private static string GetLastName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
        var parts = fullName.Split(' ');
        return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : string.Empty;
    }
}