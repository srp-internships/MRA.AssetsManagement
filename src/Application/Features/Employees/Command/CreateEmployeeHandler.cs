using MediatR;

public class CreateEmployeeCommand : IRequest<string>
{
    public CreateEmployee CreateEmployee { get; }

    public CreateEmployeeCommand(CreateEmployee createEmployee)
    {
        CreateEmployee = createEmployee;
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
        var response = await _employeeService.Create(request.CreateEmployee);
        return response;
    }
}