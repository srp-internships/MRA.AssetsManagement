using MediatR;

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