using MediatR;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Employees.Handlers;

public class GetEmployeeByEmailQuery : IRequest<EmployeeResponse>
{
    public readonly string _email;

    public GetEmployeeByEmailQuery(string email)
    {
        _email = email;
    }
}

public class GetEmployeeByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeByEmailQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<EmployeeResponse> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetByEmail(request._email);
        return response;
    }
}