using AutoMapper;
using MRA.AssetsManagement.Application.Features.Employees;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.AssetTypes.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployee, Employee>();
        CreateMap<Employee, CreateEmployee>();
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<EmployeeResponse, Employee>()
            .ForMember(d=>d.FirstName,o
                =>o.MapFrom(s=>GetSubStringBeforeSpace(s.FullName)))
            .ForMember(d=>d.LastName,o
                =>o.MapFrom(s=>GetSubStringAfterSpace(s.FullName)))
            .ForMember(d=>d.FullName,o
                =>o.MapFrom(s=>s.FullName));
    }
    private string GetSubStringBeforeSpace(string input)
    {
        string[] parts = input.Split(' ');
        return parts[0];
    }
    private string GetSubStringAfterSpace(string input)
    {
        string[] parts = input.Split(' ');
        return parts[1];
    }
}