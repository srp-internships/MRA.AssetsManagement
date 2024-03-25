using MediatR;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Employees.Handlers;

public class GetEmployeeByEmailQuery : IRequest<EmployeeResponse>
{
    public readonly string _email;
    public readonly string _token;

    public GetEmployeeByEmailQuery(string email, string token)
    {
        _email = email;
        _token = token;
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
        var response = await _employeeService.GetByEmail(request._email, request._token);
        return response;
    }
}