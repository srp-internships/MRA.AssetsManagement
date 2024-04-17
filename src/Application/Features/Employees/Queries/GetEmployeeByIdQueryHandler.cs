using MediatR;

using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;

public class GetEmployeeByIdQuery : IRequest<Employee>
{
    public string UserName { get; set; }
    public GetEmployeeByIdQuery(string userName)
    {
        UserName = userName;
    }
}
public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IEmployeeService _employeeService;
    public GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetByUserName(request.UserName);
        return response;
    }
}