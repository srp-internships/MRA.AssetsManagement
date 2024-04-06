using MediatR;

using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;

public class GetEmployeesQuery : IRequest<List<Employee>>
{
}

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeesQuery,List<Employee>>
{
    private readonly IEmployeeService _employeeService;
    public GetEmployeeQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetAll();
        return response;
    }
}