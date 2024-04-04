using AutoMapper;

using MediatR;

using MRA.AssetsManagement.Web.Shared.Employees;

public class CreateEmployeeCommand : IRequest<GetEmployee>
{
    public CreateEmployeeRequest CreateEmployeeRequest { get; }

    public CreateEmployeeCommand(CreateEmployeeRequest createEmployeeRequest)
    {
        CreateEmployeeRequest = createEmployeeRequest;
    }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, GetEmployee>
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;
    public CreateEmployeeCommandHandler(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    public async Task<GetEmployee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var response = await _employeeService.Create(request.CreateEmployeeRequest);
        return _mapper.Map<GetEmployee>(request.CreateEmployeeRequest);
    }
}