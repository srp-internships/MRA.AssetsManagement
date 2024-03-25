using MediatR;
using MRA.AssetsManagement.Domain.Entities;

public class CreateEmployeeCommand : IRequest<string>
{
    public RegisterEmployee RegisterEmployee { get; }
    public string Token { get; }

    public CreateEmployeeCommand(RegisterEmployee registerEmployee, string token)
    {
        RegisterEmployee = registerEmployee;
        Token = token;
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
        var response = await _employeeService.Create(request.RegisterEmployee, request.Token);
        return response;
    }
}