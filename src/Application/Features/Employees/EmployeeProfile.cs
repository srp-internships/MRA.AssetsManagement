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
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<CreateEmployeeRequest, GetEmployee>()
            .ForMember(d => d.FullName, o
                => o.MapFrom(s => $"{s.FirstName} {s.LastName}"));
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