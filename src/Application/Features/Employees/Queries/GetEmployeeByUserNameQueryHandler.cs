using MediatR;

using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Application.Features.Employees.Queries;

public class GetEmployeeByUserNameQuery : IRequest<Employee?>
{
    public string UserName { get; set; }
    public GetEmployeeByUserNameQuery(string userName)
    {
        UserName = userName;
    }
}
public class GetEmployeeByUserNameQueryHandler : IRequestHandler<GetEmployeeByUserNameQuery, Employee?>
{
    private readonly IEmployeeService _employeeService;
    public GetEmployeeByUserNameQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<Employee?> Handle(GetEmployeeByUserNameQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetByUserName(request.UserName);
        return response;
    }
}