using MediatR;

using MRA.AssetsManagement.Application.Common.Services.Identity.Employee;
using MRA.AssetsManagement.Web.Shared.Employees;

namespace MRA.AssetsManagement.Application.Features.Employees.Command;

public class CreateEmployeeCommand : IRequest<string>
{
    public CreateEmployeeRequest CreateEmployeeRequest { get; }

    public CreateEmployeeCommand(CreateEmployeeRequest createEmployeeRequest)
    {
        CreateEmployeeRequest = createEmployeeRequest;
    }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
{
    private readonly IEmployeeService _employeeService;
    public CreateEmployeeCommandHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.Create(request.CreateEmployeeRequest);
        return response;
    }
}